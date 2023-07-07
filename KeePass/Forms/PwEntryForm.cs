/*
  KeePass Password Safe - The Open-Source Password Manager
  Copyright (C) 2003-2022 Dominik Reichl <dominik.reichl@t-online.de>

  This program is free software; you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation; either version 2 of the License, or
  (at your option) any later version.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using KeePass.App;
using KeePass.App.Configuration;
using KeePass.Native;
using KeePass.Resources;
using KeePass.UI;
using KeePass.Util;
using KeePass.Util.MultipleValues;
using KeePass.Util.Spr;

using KeePassLib;
using KeePassLib.Collections;
using KeePassLib.Delegates;
using KeePassLib.Security;
using KeePassLib.Utility;

using NativeLib = KeePassLib.Native.NativeLib;

namespace KeePass.Forms
{
	public partial class PwEntryForm : Form
	{
		private PwEditMode m_pwEditMode = PwEditMode.Invalid;
		private PwDatabase m_pwDatabase = null;
		private bool m_bSelectFullTitle = false;

		private PwEntry m_pwEntry = null;
		private PwEntry m_pwInitialEntry = null;
		private ProtectedStringDictionary m_vStrings = null;
		private ProtectedBinaryDictionary m_vBinaries = null;
		private PwObjectList<PwEntry> m_vHistory = null;
		private StringDictionaryEx m_sdCustomData = null;

		private PwIcon m_pwEntryIcon = PwIcon.Key;
		private PwUuid m_pwCustomIconID = PwUuid.Zero;
		private ImageList m_ilIcons = null;
		private List<PwUuid> m_lOrgCustomIconIDs = new List<PwUuid>();

		private bool m_bTouchedOnce = false;

		private bool m_bInitializing = true;
		private bool m_bForceClosing = false;
		private bool m_bUrlOverrideWarning = false;

		private PwInputControlGroup m_icgPassword = new PwInputControlGroup();
		private PwGeneratorMenu m_pgmPassword = null;
		private RichTextBoxContextMenu m_ctxNotes = new RichTextBoxContextMenu();
		private ExpiryControlGroup m_cgExpiry = new ExpiryControlGroup();

		private ContextMenuStrip m_ctxBinOpen = null;
		private DynamicMenu m_dynBinOpen = null;

		private const PwIcon m_pwObjectProtected = PwIcon.PaperLocked;
		private const PwIcon m_pwObjectPlainText = PwIcon.PaperNew;

		private const PwCompareOptions m_cmpOpt = (PwCompareOptions.NullEmptyEquivStd |
			PwCompareOptions.IgnoreTimes);

		public event EventHandler<CancellableOperationEventArgs> EntrySaving;
		public event EventHandler EntrySaved;

		public bool HasModifiedEntry
		{
			get
			{
				if ((m_pwEntry == null) || (m_pwInitialEntry == null))
				{
					Debug.Assert(false);
					return true;
				}

				return !m_pwEntry.EqualsEntry(m_pwInitialEntry, m_cmpOpt,
					MemProtCmpMode.CustomOnly);
			}
		}

		private PwEntryFormTab m_eftInit = PwEntryFormTab.None;
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(PwEntryFormTab.None)]
		internal PwEntryFormTab InitialTab
		{
			// get { return m_eftInit; } // Internal, uncalled
			set { m_eftInit = value; }
		}

		private MultipleValuesEntryContext m_mvec = null;
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue((object)null)]
		public MultipleValuesEntryContext MultipleValuesEntryContext
		{
			get { return m_mvec; }
			set { m_mvec = value; }
		}

		public PwEntryForm()
		{
			InitializeComponent();

			GlobalWindowManager.InitializeForm(this);
		}

		public void InitEx(PwEntry pwEntry, PwEditMode pwMode, PwDatabase pwDatabase,
			ImageList ilIcons, bool bShowAdvancedByDefault, bool bSelectFullTitle)
		{
			Debug.Assert(pwEntry != null); if (pwEntry == null) throw new ArgumentNullException("pwEntry");
			Debug.Assert(pwMode != PwEditMode.Invalid); if (pwMode == PwEditMode.Invalid) throw new ArgumentException();
			Debug.Assert(ilIcons != null); if (ilIcons == null) throw new ArgumentNullException("ilIcons");

			m_pwEntry = pwEntry;
			m_pwEditMode = pwMode;
			m_pwDatabase = pwDatabase;
			m_ilIcons = ilIcons;
			if (bShowAdvancedByDefault) m_eftInit = PwEntryFormTab.Advanced;
			m_bSelectFullTitle = bSelectFullTitle;

			m_vStrings = m_pwEntry.Strings.CloneDeep();
			NormalizeStrings(m_vStrings, pwDatabase);

			m_vBinaries = m_pwEntry.Binaries.CloneDeep();
			m_vHistory = m_pwEntry.History.CloneDeep();

			m_lOrgCustomIconIDs.Clear();
			if (m_pwDatabase != null)
			{
				foreach (PwCustomIcon ci in m_pwDatabase.CustomIcons)
					m_lOrgCustomIconIDs.Add(ci.Uuid);
			}
		}

		private void InitEntryTab()
		{
			m_pwEntryIcon = m_pwEntry.IconId;
			m_pwCustomIconID = m_pwEntry.CustomIconUuid;

			// The user may have deleted the custom icon (using the
			// icon dialog accessible through the entry dialog and
			// then opening a history entry)
			if (!m_pwCustomIconID.Equals(PwUuid.Zero) &&
				(m_pwDatabase.GetCustomIconIndex(m_pwCustomIconID) >= 0))
			{
				// int nInx = (int)PwIcon.Count + m_pwDatabase.GetCustomIconIndex(m_pwCustomIconID);
				// if((nInx > -1) && (nInx < m_ilIcons.Images.Count))
				//	m_btnIcon.Image = m_ilIcons.Images[nInx];
				// else m_btnIcon.Image = m_ilIcons.Images[(int)m_pwEntryIcon];

				Image imgCustom = DpiUtil.GetIcon(m_pwDatabase, m_pwCustomIconID);
				// m_btnIcon.Image = (imgCustom ?? m_ilIcons.Images[(int)m_pwEntryIcon]);
				UIUtil.SetButtonImage(m_btnIcon, (imgCustom ?? m_ilIcons.Images[
					(int)m_pwEntryIcon]), true);
			}
			else
			{
				// m_btnIcon.Image = m_ilIcons.Images[(int)m_pwEntryIcon];
				UIUtil.SetButtonImage(m_btnIcon, m_ilIcons.Images[
					(int)m_pwEntryIcon], true);
			}

			bool bHideInitial = m_cbHidePassword.Checked;
			m_icgPassword.Attach(m_tbPassword, m_cbHidePassword, m_lblPasswordRepeat,
				m_tbRepeatPassword, m_lblQuality, m_pbQuality, m_lblQualityInfo,
				m_ttRect, this, bHideInitial, false);
			m_icgPassword.ContextDatabase = m_pwDatabase;
			m_icgPassword.ContextEntry = m_pwEntry;
			m_icgPassword.IsSprVariant = true;

			GFunc<PwEntry> fGetContextEntry = delegate ()
			{
				return PwEntry.CreateVirtual(m_pwEntry.ParentGroup ??
					new PwGroup(true, true), m_vStrings);
			};
			m_pgmPassword = new PwGeneratorMenu(m_btnGenPw, m_ttRect, m_icgPassword,
				fGetContextEntry, m_pwDatabase, (m_mvec != null));

			m_cbQualityCheck.Image = GfxUtil.ScaleImage(Properties.Resources.B16x16_MessageBox_Info,
				DpiUtil.ScaleIntX(12), DpiUtil.ScaleIntY(12), ScaleTransformFlags.UIIcon);
			m_cbQualityCheck.Checked = m_pwEntry.QualityCheck;
			OnQualityCheckCheckedChanged(null, EventArgs.Empty);
			if ((Program.Config.UI.UIFlags & (ulong)AceUIFlags.HidePwQuality) != 0)
				m_cbQualityCheck.Visible = false;

			if (m_pwEntry.Expires)
			{
				m_dtExpireDateTime.Value = TimeUtil.ToLocal(m_pwEntry.ExpiryTime, true);
				UIUtil.SetChecked(m_cbExpires, true);
			}
			else // Does not expire
			{
				m_dtExpireDateTime.Value = DateTime.Now.Date;
				UIUtil.SetChecked(m_cbExpires, false);
			}
			m_cgExpiry.Attach(m_cbExpires, m_dtExpireDateTime);

			if (m_pwEditMode == PwEditMode.ViewReadOnlyEntry)
			{
				m_tbTitle.ReadOnly = m_tbUserName.ReadOnly = m_tbPassword.ReadOnly =
					m_tbRepeatPassword.ReadOnly = m_tbUrl.ReadOnly =
					m_rtNotes.ReadOnly = true;

				UIUtil.SetEnabledFast(false, m_btnIcon, m_btnGenPw, m_cbQualityCheck,
					m_cbExpires, m_dtExpireDateTime);

				// m_rtNotes.SelectAll();
				// m_rtNotes.BackColor = m_rtNotes.SelectionBackColor =
				//	AppDefs.ColorControlDisabled;
				// m_rtNotes.DeselectAll();
			}

			// Show URL in blue, if it's black in the current visual theme
			if (UIUtil.ColorsEqual(m_tbUrl.ForeColor, Color.Black))
				m_tbUrl.ForeColor = Color.Blue;
		}

		private void InitAdvancedTab()
		{
			m_lvStrings.SmallImageList = m_ilIcons;
			// m_lvBinaries.SmallImageList = m_ilIcons;

			int nWidth = m_lvStrings.ClientSize.Width / 2;
			m_lvStrings.Columns.Add(KPRes.Name, nWidth);
			m_lvStrings.Columns.Add(KPRes.Value, nWidth);

			nWidth = m_lvBinaries.ClientSize.Width / 2;
			m_lvBinaries.Columns.Add(KPRes.Attachments, nWidth);
			m_lvBinaries.Columns.Add(KPRes.Size, nWidth, HorizontalAlignment.Right);

			if (m_pwEditMode == PwEditMode.ViewReadOnlyEntry)
			{
				m_btnStrAdd.Enabled = false;
				m_btnStrEdit.Text = KPRes.ViewCmd;
				m_lvBinaries.LabelEdit = false;
			}
			if ((m_pwEditMode == PwEditMode.ViewReadOnlyEntry) || (m_mvec != null))
				m_btnBinAdd.Enabled = false;
		}

		public void UpdateEntryStrings(bool bGuiToInternal, bool bSetRepeatPw,
			bool bUpdateState)
		{
			if (bGuiToInternal)
			{
				m_vStrings.Set(PwDefs.TitleField, new ProtectedString(m_pwDatabase.MemoryProtection.ProtectTitle, m_tbTitle.Text));
				m_vStrings.Set(PwDefs.UserNameField, new ProtectedString(m_pwDatabase.MemoryProtection.ProtectUserName, m_tbUserName.Text));
				m_vStrings.Set(PwDefs.PasswordField, new ProtectedString(m_pwDatabase.MemoryProtection.ProtectPassword, m_tbPassword.Text));
				m_vStrings.Set(PwDefs.UrlField, new ProtectedString(m_pwDatabase.MemoryProtection.ProtectUrl, m_tbUrl.Text));
				m_vStrings.Set(PwDefs.NotesField, new ProtectedString(m_pwDatabase.MemoryProtection.ProtectNotes, m_rtNotes.Text));

				NormalizeStrings(m_vStrings, m_pwDatabase);
			}
			else // Internal to GUI
			{
				m_tbTitle.Text = m_vStrings.ReadSafe(PwDefs.TitleField);
				m_tbUserName.Text = m_vStrings.ReadSafe(PwDefs.UserNameField);

				string ps = m_vStrings.GetSafe(PwDefs.PasswordField).ReadString();
				m_icgPassword.SetPassword(ps, bSetRepeatPw);

				m_tbUrl.Text = m_vStrings.ReadSafe(PwDefs.UrlField);
				m_rtNotes.Text = m_vStrings.ReadSafe(PwDefs.NotesField);

				ProtectedString psCue = MultipleValuesEx.CueProtectedString;

				UIScrollInfo s = UIUtil.GetScrollInfo(m_lvStrings, true);
				m_lvStrings.BeginUpdate();
				m_lvStrings.Items.Clear();
				foreach (KeyValuePair<string, ProtectedString> kvp in m_vStrings)
				{
					if (PwDefs.IsStandardField(kvp.Key)) continue;

					bool bProt = kvp.Value.IsProtected;
					PwIcon pwIcon = (bProt ? m_pwObjectProtected : m_pwObjectPlainText);

					ListViewItem lvi = m_lvStrings.Items.Add(kvp.Key, (int)pwIcon);

					if ((m_mvec != null) && kvp.Value.Equals(psCue, false))
					{
						lvi.SubItems.Add(MultipleValuesEx.CueString);
						MultipleValuesEx.ConfigureText(lvi, 1);
					}
					else if (bProt)
						lvi.SubItems.Add(PwDefs.HiddenPassword);
					else
						lvi.SubItems.Add(StrUtil.MultiToSingleLine(
							kvp.Value.ReadString()));
				}
				UIUtil.Scroll(m_lvStrings, s, false);
				m_lvStrings.EndUpdate();
			}

			if (bUpdateState) EnableControlsEx();
		}

		public void UpdateEntryBinaries(bool bGuiToInternal, bool bUpdateState)
		{
			UpdateEntryBinaries(bGuiToInternal, bUpdateState, null);
		}

		public void UpdateEntryBinaries(bool bGuiToInternal, bool bUpdateState,
			string strFocusItem)
		{
			if (bGuiToInternal) { }
			else // Internal to GUI
			{
				UIScrollInfo s = UIUtil.GetScrollInfo(m_lvBinaries, true);
				m_lvBinaries.BeginUpdate();
				m_lvBinaries.Items.Clear();
				foreach (KeyValuePair<string, ProtectedBinary> kvpBin in m_vBinaries)
				{
					// PwIcon pwIcon = (kvpBin.Value.IsProtected ?
					//	m_pwObjectProtected : m_pwObjectPlainText);
					ListViewItem lvi = m_lvBinaries.Items.Add(kvpBin.Key); // , (int)pwIcon);
					lvi.SubItems.Add(StrUtil.FormatDataSizeKB(kvpBin.Value.Length));
				}
				FileIcons.UpdateImages(m_lvBinaries, null);
				UIUtil.Scroll(m_lvBinaries, s, false);

				if (strFocusItem != null)
				{
					ListViewItem lvi = m_lvBinaries.FindItemWithText(strFocusItem,
						false, 0, false);
					if (lvi != null)
					{
						m_lvBinaries.EnsureVisible(lvi.Index);
						UIUtil.SetFocusedItem(m_lvBinaries, lvi, true);
					}
					else { Debug.Assert(false); }
				}

				m_lvBinaries.EndUpdate();
			}

			if (bUpdateState) EnableControlsEx();
		}

		private void InitPropertiesTab()
		{
			TagUtil.MakeInheritedTagsLink(m_linkTagsInh, m_pwEntry.ParentGroup, this);
			m_tbTags.Text = StrUtil.TagsToString(m_pwEntry.Tags, true);
			TagUtil.MakeTagsButton(m_btnTags, m_tbTags, m_ttRect, m_pwEntry.ParentGroup,
				((m_pwDatabase != null) ? m_pwDatabase.RootGroup : null));

			// https://sourceforge.net/p/keepass/discussion/329220/thread/f98dece5/
			if (Program.Translation.Properties.RightToLeft)
				m_cmbOverrideUrl.RightToLeft = RightToLeft.No;
			m_cmbOverrideUrl.Text = m_pwEntry.OverrideUrl;

			m_sdCustomData = m_pwEntry.CustomData.CloneDeep();
			UIUtil.StrDictListInit(m_lvCustomData);
			UIUtil.StrDictListUpdate(m_lvCustomData, m_sdCustomData, (m_mvec != null));

#if DEBUG
			m_lvCustomData.KeyDown += delegate (object sender, KeyEventArgs e)
			{
				if (e.KeyData == (Keys.Control | Keys.F5))
				{
					UIUtil.SetHandled(e, true);
					m_sdCustomData.Set("Example_Constant", "Constant value");
					m_sdCustomData.Set("Example_Random", Program.GlobalRandom.Next().ToString());
					UIUtil.StrDictListUpdate(m_lvCustomData, m_sdCustomData, (m_mvec != null));
				}
			};
			m_lvCustomData.KeyUp += delegate (object sender, KeyEventArgs e)
			{
				if (e.KeyData == (Keys.Control | Keys.F5))
					UIUtil.SetHandled(e, true);
			};
#endif

			m_tbUuid.Text = m_pwEntry.Uuid.ToHexString() + ", " +
				Convert.ToBase64String(m_pwEntry.Uuid.UuidBytes);

			if (m_pwEditMode == PwEditMode.ViewReadOnlyEntry)
			{
				m_tbTags.ReadOnly = true;
				m_btnTags.Enabled = false;
				m_cmbOverrideUrl.Enabled = false;
			}
		}

		private void InitHistoryTab()
		{
			m_lblCreatedData.Text = TimeUtil.ToDisplayString(m_pwEntry.CreationTime);
			m_lblModifiedData.Text = TimeUtil.ToDisplayString(m_pwEntry.LastModificationTime);

			m_lvHistory.SmallImageList = m_ilIcons;

			m_lvHistory.Columns.Add(KPRes.Version);
			m_lvHistory.Columns.Add(KPRes.Title);
			m_lvHistory.Columns.Add(KPRes.UserName);
			m_lvHistory.Columns.Add(KPRes.Size, 72, HorizontalAlignment.Right);

			UpdateHistoryList(false);

			if ((m_pwEditMode == PwEditMode.ViewReadOnlyEntry) || (m_mvec != null))
			{
				m_lblPrev.Enabled = false;
				m_lvHistory.Enabled = false;
			}
		}

		private void UpdateHistoryList(bool bUpdateState)
		{
			UIScrollInfo s = UIUtil.GetScrollInfo(m_lvHistory, true);

			ImageList ilIcons = m_lvHistory.SmallImageList;
			int ci = ((ilIcons != null) ? ilIcons.Images.Count : 0);

			m_lvHistory.BeginUpdate();
			m_lvHistory.Items.Clear();

			foreach (PwEntry pe in m_vHistory)
			{
				ListViewItem lvi = m_lvHistory.Items.Add(TimeUtil.ToDisplayString(
					pe.LastModificationTime));

				int idxIcon = (int)pe.IconId;
				PwUuid pu = pe.CustomIconUuid;
				if (!pu.Equals(PwUuid.Zero))
				{
					// The user may have deleted the custom icon (using
					// the icon dialog accessible through this entry
					// dialog); continuing to show the deleted custom
					// icon would be confusing
					int idxNew = m_pwDatabase.GetCustomIconIndex(pu);
					if (idxNew >= 0) // Icon not deleted
					{
						int idxOrg = m_lOrgCustomIconIDs.IndexOf(pu);
						if (idxOrg >= 0) idxIcon = (int)PwIcon.Count + idxOrg;
						else { Debug.Assert(false); }
					}
				}
				if (idxIcon < ci) lvi.ImageIndex = idxIcon;
				else { Debug.Assert(false); }

				lvi.SubItems.Add(pe.Strings.ReadSafe(PwDefs.TitleField));
				lvi.SubItems.Add(pe.Strings.ReadSafe(PwDefs.UserNameField));
				lvi.SubItems.Add(StrUtil.FormatDataSizeKB(pe.GetSize()));
			}

			UIUtil.Scroll(m_lvHistory, s, true);
			m_lvHistory.EndUpdate();

			if (bUpdateState) EnableControlsEx();
		}

		private void ResizeColumnHeaders()
		{
			UIUtil.ResizeColumns(m_lvStrings, true);
			UIUtil.ResizeColumns(m_lvBinaries, new int[] { 4, 1 }, true);
			UIUtil.ResizeColumns(m_lvHistory, new int[] { 21, 20, 18, 11 }, true);
		}

		private void OnFormLoad(object sender, EventArgs e)
		{
			if (m_pwEntry == null) { Debug.Assert(false); throw new InvalidOperationException(); }
			if (m_pwEditMode == PwEditMode.Invalid) { Debug.Assert(false); throw new InvalidOperationException(); }
			if (m_pwDatabase == null) { Debug.Assert(false); throw new InvalidOperationException(); }
			if (m_ilIcons == null) { Debug.Assert(false); throw new InvalidOperationException(); }

			m_bInitializing = true;

			// If there is an intermediate form, the custom icons
			// in the image list may be outdated
			Form fTop = GlobalWindowManager.TopWindow;
			Debug.Assert(fTop != this); // Before adding ourself
			if ((fTop != null) && (fTop != Program.MainForm))
				m_lOrgCustomIconIDs.Clear();

			GlobalWindowManager.AddWindow(this);

			m_pwInitialEntry = m_pwEntry.CloneDeep();
			NormalizeStrings(m_pwInitialEntry.Strings, m_pwDatabase);

			UIUtil.ConfigureToolTip(m_ttRect);
			UIUtil.SetToolTip(m_ttRect, m_btnIcon, KPRes.SelectIcon, true);
			UIUtil.SetToolTip(m_ttRect, m_cbQualityCheck, KPRes.QualityCheckToggle, true);

			UIUtil.ConfigureToolTip(m_ttBalloon);

			m_ctxNotes.Attach(m_rtNotes, this);

			m_ctxBinOpen = new ContextMenuStrip();
			m_ctxBinOpen.Opening += OnCtxBinOpenOpening;
			m_dynBinOpen = new DynamicMenu(m_ctxBinOpen.Items);
			m_dynBinOpen.MenuClick += OnDynBinOpen;

			string strTitle = string.Empty, strDesc = string.Empty;
			switch (m_pwEditMode)
			{
				case PwEditMode.AddNewEntry:
					strTitle = KPRes.AddEntry;
					strDesc = KPRes.AddEntryDesc;
					break;
				case PwEditMode.EditExistingEntry:
					if (m_mvec != null)
					{
						strTitle = KPRes.EditEntries + " (" +
							m_mvec.Entries.Length.ToString() + ")";
						strDesc = KPRes.EditEntriesDesc;
					}
					else
					{
						strTitle = KPRes.EditEntry;
						strDesc = KPRes.EditEntryDesc;
					}
					break;
				case PwEditMode.ViewReadOnlyEntry:
					strTitle = KPRes.ViewEntryReadOnly;
					strDesc = KPRes.ViewEntryDesc;
					break;
				default:
					Debug.Assert(false);
					break;
			}

			BannerFactory.CreateBannerEx(this, m_bannerImage,
				Properties.Resources.B48x48_KGPG_Sign, strTitle, strDesc);
			Icon = AppIcons.Default;
			Text = strTitle;

			UIUtil.PrepareStandardMultilineControl(m_rtNotes, true, true);

			bool bForceHide = !AppPolicy.Current.UnhidePasswords;
			if (Program.Config.UI.Hiding.SeparateHidingSettings)
				m_cbHidePassword.Checked = (Program.Config.UI.Hiding.HideInEntryWindow || bForceHide);
			else
			{
				AceColumn colPw = Program.Config.MainWindow.FindColumn(AceColumnType.Password);
				m_cbHidePassword.Checked = (((colPw != null) ? colPw.HideWithAsterisks :
					true) || bForceHide);
			}

			InitEntryTab();
			InitAdvancedTab();
			InitPropertiesTab();
			InitHistoryTab();

			UpdateEntryStrings(false, true, false);
			UpdateEntryBinaries(false, false);

			if (m_mvec != null)
			{
				MultipleValuesEx.ConfigureText(m_tbTitle, true);
				MultipleValuesEx.ConfigureText(m_tbUserName, true);
				MultipleValuesEx.ConfigureText(m_tbPassword, true);
				MultipleValuesEx.ConfigureText(m_tbRepeatPassword, true);
				m_cbQualityCheck.Enabled = false;
				MultipleValuesEx.ConfigureText(m_tbUrl, true);
				MultipleValuesEx.ConfigureText(m_rtNotes, true);
				if (m_mvec.MultiExpiry)
					MultipleValuesEx.ConfigureState(m_cbExpires, true);

				UIUtil.SetEnabledFast(false, m_grpAttachments, m_lvBinaries);

				MultipleValuesEx.ConfigureText(m_tbTags, true);
				MultipleValuesEx.ConfigureText(m_cmbOverrideUrl, true);
				m_tbUuid.Text = MultipleValuesEx.CueString;
				UIUtil.SetEnabledFast(false, m_lblUuid, m_tbUuid);

				m_lblCreatedData.Text = MultipleValuesEx.CueString;
				UIUtil.SetEnabledFast(false, m_lblCreated, m_lblCreatedData);
				m_lblModifiedData.Text = MultipleValuesEx.CueString;
				UIUtil.SetEnabledFast(false, m_lblModified, m_lblModifiedData);
			}

			m_bInitializing = false;

			ResizeColumnHeaders();
			EnableControlsEx();

			ThreadPool.QueueUserWorkItem(delegate (object state)
			{
				try
				{
					InitUserNameSuggestions();
					InitOverridesBox();
				}
				catch (Exception) { Debug.Assert(false); }
			});

			if (MonoWorkarounds.IsRequired(2140)) Application.DoEvents();

			if (m_pwEditMode == PwEditMode.ViewReadOnlyEntry)
			{
				UIUtil.SetFocus(m_btnCancel, this);
				m_btnOK.Enabled = false;
			}
			else
			{
				if (m_bSelectFullTitle) m_tbTitle.Select(0, m_tbTitle.TextLength);
				else m_tbTitle.Select(0, 0);

				UIUtil.SetFocus(m_tbTitle, this);
			}

			switch (m_eftInit)
			{
				case PwEntryFormTab.Advanced:
					m_tabMain.SelectedTab = m_tabAdvanced; break;
				case PwEntryFormTab.Properties:
					m_tabMain.SelectedTab = m_tabProperties; break;
				case PwEntryFormTab.History:
					m_tabMain.SelectedTab = m_tabHistory; break;
				default: break;
			}
		}

		private void EnableControlsEx()
		{
			if (m_bInitializing) return;

			bool bEdit = (m_pwEditMode != PwEditMode.ViewReadOnlyEntry);
			bool bMulti = (m_mvec != null);

			int nStrings = m_lvStrings.Items.Count;
			int nStringsSel = m_lvStrings.SelectedIndices.Count;
			int nBinSel = m_lvBinaries.SelectedIndices.Count;
			int nHistorySel = m_lvHistory.SelectedIndices.Count;

			m_btnStrEdit.Enabled = (nStringsSel == 1); // Supports read-only
			m_btnStrDelete.Enabled = (bEdit && (nStringsSel >= 1));

			m_btnBinDelete.Enabled = (bEdit && (nBinSel >= 1));
			m_btnBinOpen.Enabled = (nBinSel == 1);
			m_btnBinSave.Enabled = (nBinSel >= 1);

			bool bUrlEmpty = (m_tbUrl.TextLength == 0);
			bool bUrlOverrideEmpty = (m_cmbOverrideUrl.Text.Length == 0);
			bool bWarn = (bUrlEmpty && !bUrlOverrideEmpty);
			if (bWarn != m_bUrlOverrideWarning)
			{
				if (bWarn) m_cmbOverrideUrl.BackColor = AppDefs.ColorEditError;
				else m_cmbOverrideUrl.ResetBackColor();

				UIUtil.SetToolTip(m_ttBalloon, m_cmbOverrideUrl, (bWarn ?
					KPRes.UrlFieldEmptyFirstTab : string.Empty), false);

				m_bUrlOverrideWarning = bWarn;
			}

			m_btnCDDel.Enabled = (bEdit && (m_lvCustomData.SelectedIndices.Count > 0));

			UIUtil.SetEnabledFast((bEdit && !bMulti && (nHistorySel == 1)),
				m_btnHistoryView, m_btnHistoryRestore);
			m_btnHistoryDelete.Enabled = (bEdit && !bMulti && (nHistorySel >= 1));
		}

		private bool SaveEntry(PwEntry peTarget, bool bValidate)
		{
			if (m_pwEditMode == PwEditMode.ViewReadOnlyEntry) { Debug.Assert(false); return true; }

			if (bValidate && !m_icgPassword.ValidateData(true)) return false;

			bool bPri = ReferenceEquals(peTarget, m_pwEntry);

			if ((EntrySaving != null) && bPri)
			{
				CancellableOperationEventArgs e = new CancellableOperationEventArgs();
				EntrySaving(this, e);
				if (e.Cancel) return false;
			}

			peTarget.History = m_vHistory; // Must be assigned before CreateBackup()
										   // Create a backup only if bPri, because it modifies m_vHistory;
										   // https://sourceforge.net/p/keepass/bugs/2062/
			bool bCreateBackup = ((m_pwEditMode != PwEditMode.AddNewEntry) && bPri);
			if (bCreateBackup) peTarget.CreateBackup(null);

			UpdateEntryStrings(true, false, false);
			peTarget.Strings = m_vStrings;

			peTarget.IconId = m_pwEntryIcon;
			peTarget.CustomIconUuid = m_pwCustomIconID;

			peTarget.QualityCheck = m_cbQualityCheck.Checked;

			peTarget.Expires = m_cgExpiry.Checked;
			if (peTarget.Expires) peTarget.ExpiryTime = m_cgExpiry.Value;

			peTarget.Binaries = m_vBinaries;

			peTarget.Tags = StrUtil.StringToTags(m_tbTags.Text);
			peTarget.OverrideUrl = m_cmbOverrideUrl.Text;
			peTarget.CustomData = m_sdCustomData;

			peTarget.Touch(true, false); // Touch *after* backup
			if (bPri) m_bTouchedOnce = true;

			bool bUndoBackup = false;
			PwCompareOptions cmpOpt = m_cmpOpt;
			if (bCreateBackup) cmpOpt |= PwCompareOptions.IgnoreLastBackup;
			if (peTarget.EqualsEntry(m_pwInitialEntry, cmpOpt, MemProtCmpMode.CustomOnly))
			{
				// No modifications at all => restore last mod time and undo backup
				peTarget.LastModificationTime = m_pwInitialEntry.LastModificationTime;
				bUndoBackup = bCreateBackup;
			}
			else if (bCreateBackup)
			{
				// If only history items have been modified (deleted) => undo
				// backup, but without restoring the last mod time
				PwCompareOptions cmpOptNH = (m_cmpOpt | PwCompareOptions.IgnoreHistory);
				if (peTarget.EqualsEntry(m_pwInitialEntry, cmpOptNH, MemProtCmpMode.CustomOnly))
					bUndoBackup = true;
			}
			if (bUndoBackup) peTarget.History.RemoveAt(peTarget.History.UCount - 1);

			if (bPri) peTarget.MaintainBackups(m_pwDatabase);

			if (m_mvec != null)
			{
				m_mvec.MultiExpiry = (m_cbExpires.CheckState == CheckState.Indeterminate);
			}

			if ((EntrySaved != null) && bPri)
				EntrySaved(this, EventArgs.Empty);

			return true;
		}

		private void OnBtnOK(object sender, EventArgs e)
		{
			if (SaveEntry(m_pwEntry, true)) m_bForceClosing = true;
			else DialogResult = DialogResult.None;
		}

		private void OnBtnCancel(object sender, EventArgs e)
		{
			m_bForceClosing = true;

			try
			{
				ushort usEsc = NativeMethods.GetAsyncKeyState((int)Keys.Escape);
				if ((usEsc & 0x8000) != 0) m_bForceClosing = false;
			}
			catch (Exception) { Debug.Assert(NativeLib.IsUnix()); }
		}

		private void OnFormClosed(object sender, FormClosedEventArgs e)
		{
			if (!m_bTouchedOnce) m_pwEntry.Touch(false, false);

			if (m_pwEditMode != PwEditMode.ViewReadOnlyEntry)
				Program.Config.UI.Hiding.HideInEntryWindow = m_cbHidePassword.Checked;

			m_icgPassword.Release();

			if (m_pgmPassword != null)
			{
				m_pgmPassword.Dispose();
				m_pgmPassword = null;
			}
			else { Debug.Assert(false); }

			m_ctxNotes.Detach();
			m_cgExpiry.Release();

			m_dynBinOpen.MenuClick -= OnDynBinOpen;
			m_dynBinOpen.Clear();
			m_ctxBinOpen.Opening -= OnCtxBinOpenOpening;
			m_ctxBinOpen.Dispose();

			// Detach event handlers
			m_lvStrings.SmallImageList = null;
			// m_lvBinaries.SmallImageList = null;
			FileIcons.ReleaseImages(m_lvBinaries);
			m_lvHistory.SmallImageList = null;

			GlobalWindowManager.RemoveWindow(this);
		}

		private void OnBtnStrAdd(object sender, EventArgs e)
		{
			UpdateEntryStrings(true, false, false);

			EditStringForm esf = new EditStringForm();
			esf.InitEx(m_vStrings, null, null, m_pwDatabase);

			if (UIUtil.ShowDialogAndDestroy(esf) == DialogResult.OK)
			{
				UpdateEntryStrings(false, false, true);
				ResizeColumnHeaders();
			}
		}

		private void OnBtnStrEdit(object sender, EventArgs e)
		{
			ListView.SelectedListViewItemCollection vSel = m_lvStrings.SelectedItems;
			if (vSel.Count <= 0) return;

			UpdateEntryStrings(true, false, false);

			string strName = vSel[0].Text;
			ProtectedString psValue = m_vStrings.Get(strName);
			if (psValue == null) { Debug.Assert(false); return; }

			EditStringForm esf = new EditStringForm();
			esf.InitEx(m_vStrings, strName, psValue, m_pwDatabase);
			esf.ReadOnlyEx = (m_pwEditMode == PwEditMode.ViewReadOnlyEntry);
			esf.MultipleValuesEntryContext = m_mvec;

			if (UIUtil.ShowDialogAndDestroy(esf) == DialogResult.OK)
				UpdateEntryStrings(false, false, true);
		}

		private void OnBtnStrDelete(object sender, EventArgs e)
		{
			UpdateEntryStrings(true, false, false);

			ListView.SelectedListViewItemCollection lvsc = m_lvStrings.SelectedItems;
			foreach (ListViewItem lvi in lvsc)
			{
				if (!m_vStrings.Remove(lvi.Text)) { Debug.Assert(false); }
			}

			if (lvsc.Count > 0)
			{
				UpdateEntryStrings(false, false, true);
				ResizeColumnHeaders();
			}
		}

		private void OnBtnBinAdd(object sender, EventArgs e)
		{
			OpenFileDialogEx ofd = UIUtil.CreateOpenFileDialog(KPRes.AttachFiles,
			UIUtil.CreateFileTypeFilter(null, null, true), 1, null, true,
			AppDefs.FileDialogContext.Attachments);

			if (ofd.ShowDialog() == DialogResult.OK)
				BinImportFiles(ofd.FileNames);
		}

		private void OnBtnBinDelete(object sender, EventArgs e)
		{
			UpdateEntryBinaries(true, false);

			ListView.SelectedListViewItemCollection lvsc = m_lvBinaries.SelectedItems;
			foreach (ListViewItem lvi in lvsc)
			{
				if (!m_vBinaries.Remove(lvi.Text)) { Debug.Assert(false); }
			}

			if (lvsc.Count > 0)
			{
				UpdateEntryBinaries(false, true);
				ResizeColumnHeaders();
			}
		}

		private void OnBtnBinSave(object sender, EventArgs e)
		{
			ListView.SelectedListViewItemCollection lvsc = m_lvBinaries.SelectedItems;

			int nSelCount = lvsc.Count;
			if (nSelCount == 0) { Debug.Assert(false); return; }

			if (nSelCount == 1)
			{
				SaveFileDialogEx sfd = UIUtil.CreateSaveFileDialog(KPRes.AttachmentSave,
					UrlUtil.GetSafeFileName(lvsc[0].Text), UIUtil.CreateFileTypeFilter(
					null, null, true), 1, null, AppDefs.FileDialogContext.Attachments);

				if (sfd.ShowDialog() == DialogResult.OK)
					SaveAttachmentTo(lvsc[0], sfd.FileName, false);
			}
			else // nSelCount > 1
			{
				FolderBrowserDialog fbd = UIUtil.CreateFolderBrowserDialog(KPRes.AttachmentsSave);

				if (fbd.ShowDialog() == DialogResult.OK)
				{
					string strRootPath = UrlUtil.EnsureTerminatingSeparator(
						fbd.SelectedPath, false);

					foreach (ListViewItem lvi in lvsc)
						SaveAttachmentTo(lvi, strRootPath + UrlUtil.GetSafeFileName(
							lvi.Text), true);
				}
				fbd.Dispose();
			}
		}

		private void SaveAttachmentTo(ListViewItem lvi, string strFile,
			bool bConfirmOverwrite)
		{
			if (lvi == null) { Debug.Assert(false); return; }
			if (string.IsNullOrEmpty(strFile)) { Debug.Assert(false); return; }

			if (bConfirmOverwrite && File.Exists(strFile))
			{
				string strMsg = KPRes.FileExistsAlready + MessageService.NewLine +
					strFile + MessageService.NewParagraph +
					KPRes.OverwriteExistingFileQuestion;

				if (!MessageService.AskYesNo(strMsg)) return;
			}

			ProtectedBinary pb = m_vBinaries.Get(lvi.Text);
			if (pb == null) { Debug.Assert(false); return; }

			byte[] pbData = pb.ReadData();
			try { File.WriteAllBytes(strFile, pbData); }
			catch (Exception exWrite)
			{
				MessageService.ShowWarning(strFile, exWrite);
			}
			if (pb.IsProtected) MemUtil.ZeroByteArray(pbData);
		}

		private void OnBtnHistoryView(object sender, EventArgs e)
		{
			Debug.Assert(m_vHistory.UCount == m_lvHistory.Items.Count);

			ListView.SelectedIndexCollection lvsic = m_lvHistory.SelectedIndices;
			if (lvsic.Count != 1) { Debug.Assert(false); return; }

			PwEntry pe = m_vHistory.GetAt((uint)lvsic[0]);
			if (pe == null) { Debug.Assert(false); return; }

			PwEntryForm pwf = new PwEntryForm();
			pwf.InitEx(pe, PwEditMode.ViewReadOnlyEntry, m_pwDatabase,
				m_ilIcons, false, false);

			UIUtil.ShowDialogAndDestroy(pwf);
		}

		private void OnBtnHistoryDelete(object sender, EventArgs e)
		{
			Debug.Assert(m_vHistory.UCount == m_lvHistory.Items.Count);

			ListView.SelectedIndexCollection lvsic = m_lvHistory.SelectedIndices;
			int n = lvsic.Count; // Getting Count sends a message
			if (n == 0) return;

			// LVSIC: one access by index requires O(n) time, thus copy
			// all to an array (which requires O(1) for each element)
			int[] v = new int[n];
			lvsic.CopyTo(v, 0);

			for (int i = 0; i < n; ++i)
				m_vHistory.RemoveAt((uint)v[n - i - 1]);

			UpdateHistoryList(true);
			ResizeColumnHeaders();
		}

		private void OnBtnHistoryRestore(object sender, EventArgs e)
		{
			Debug.Assert(m_vHistory.UCount == m_lvHistory.Items.Count);

			ListView.SelectedIndexCollection lvsic = m_lvHistory.SelectedIndices;
			if (lvsic.Count != 1) { Debug.Assert(false); return; }

			m_pwEntry.RestoreFromBackup((uint)lvsic[0], m_pwDatabase);
			m_pwEntry.Touch(true, false);
			m_bTouchedOnce = true;
			DialogResult = DialogResult.OK; // Doesn't invoke OnBtnOK
		}

		private void OnHistorySelectedIndexChanged(object sender, EventArgs e)
		{
			EnableControlsEx();
		}

		private void OnStringsSelectedIndexChanged(object sender, EventArgs e)
		{
			EnableControlsEx();
		}

		private void OnBinariesSelectedIndexChanged(object sender, EventArgs e)
		{
			EnableControlsEx();
		}

		private void OnBtnPickIcon(object sender, EventArgs e)
		{
			IconPickerForm ipf = new IconPickerForm();
			ipf.InitEx(m_ilIcons, (uint)PwIcon.Count, m_pwDatabase,
				(uint)m_pwEntryIcon, m_pwCustomIconID);

			if (ipf.ShowDialog() == DialogResult.OK)
			{
				m_pwEntryIcon = (PwIcon)ipf.ChosenIconId;
				m_pwCustomIconID = ipf.ChosenCustomIconUuid;

				if (!m_pwCustomIconID.Equals(PwUuid.Zero))
					UIUtil.SetButtonImage(m_btnIcon, DpiUtil.GetIcon(
						m_pwDatabase, m_pwCustomIconID), true);
				else
					UIUtil.SetButtonImage(m_btnIcon, m_ilIcons.Images[
						(int)m_pwEntryIcon], true);
			}

			UIUtil.DestroyForm(ipf);

			UpdateHistoryList(true); // User may have deleted a custom icon
		}

		private void OnStringsItemActivate(object sender, EventArgs e)
		{
			OnBtnStrEdit(sender, e);
		}

		private bool GetSelBin(out string strDataItem, out ProtectedBinary pb)
		{
			strDataItem = null;
			pb = null;

			ListView.SelectedListViewItemCollection lvsic = m_lvBinaries.SelectedItems;
			if ((lvsic == null) || (lvsic.Count != 1)) return false; // No assert

			strDataItem = lvsic[0].Text;
			pb = m_vBinaries.Get(strDataItem);
			if (pb == null) { Debug.Assert(false); return false; }

			return true;
		}

		private void OpenSelBin(BinaryDataOpenOptions optBase)
		{
			string strDataItem;
			ProtectedBinary pb;
			if (!GetSelBin(out strDataItem, out pb)) return;

			BinaryDataOpenOptions opt = ((optBase != null) ? optBase.CloneDeep() :
				new BinaryDataOpenOptions());
			if (m_pwEditMode == PwEditMode.ViewReadOnlyEntry)
			{
				if (optBase == null)
					opt.Handler = BinaryDataHandler.InternalViewer;

				opt.ReadOnly = true;
			}

			ProtectedBinary pbMod = BinaryDataUtil.Open(strDataItem, pb, opt);
			if (pbMod != null)
			{
				m_vBinaries.Set(strDataItem, pbMod);
				UpdateEntryBinaries(false, true, strDataItem); // Update size
			}
		}

		private void OnBtnBinOpen(object sender, EventArgs e)
		{
			OpenSelBin(null);
		}

		private void OnDynBinOpen(object sender, DynamicMenuEventArgs e)
		{
			if (e == null) { Debug.Assert(false); return; }

			BinaryDataOpenOptions opt = (e.Tag as BinaryDataOpenOptions);
			if (opt == null) { Debug.Assert(false); return; }

			OpenSelBin(opt);
		}

		private void OnCtxBinOpenOpening(object sender, CancelEventArgs e)
		{
			string strDataItem;
			ProtectedBinary pb;
			if (!GetSelBin(out strDataItem, out pb))
			{
				e.Cancel = true;
				return;
			}

			BinaryDataUtil.BuildOpenWithMenu(m_dynBinOpen, strDataItem, pb,
				(m_pwEditMode == PwEditMode.ViewReadOnlyEntry));
		}

		private bool m_bClosing = false; // Mono bug workaround
		private void OnFormClosing(object sender, FormClosingEventArgs e)
		{
			if (m_bClosing) return;
			m_bClosing = true;
			HandleFormClosing(e);
			m_bClosing = false;
		}

		private void HandleFormClosing(FormClosingEventArgs e)
		{
			bool bCancel = false;
			if (!m_bForceClosing && (m_pwEditMode != PwEditMode.ViewReadOnlyEntry))
			{
				PwEntry pe = m_pwInitialEntry.CloneDeep();
				SaveEntry(pe, false);

				bool bModified = !pe.EqualsEntry(m_pwInitialEntry, m_cmpOpt,
					MemProtCmpMode.CustomOnly);
				bModified |= !m_icgPassword.ValidateData(false);

				if (bModified)
				{
					string strTitle = pe.Strings.ReadSafe(PwDefs.TitleField).Trim();
					string strHeader = ((strTitle.Length == 0) ? string.Empty :
						(KPRes.Entry + @" '" + strTitle + @"'"));
					string strText = KPRes.SaveBeforeCloseEntry;

					if (m_mvec != null)
					{
						strHeader = string.Empty;
						strText = KPRes.SaveBeforeCloseQuestion;
					}

					VistaTaskDialog dlg = new VistaTaskDialog();
					dlg.CommandLinks = false;
					dlg.Content = strText;
					dlg.MainInstruction = strHeader;
					dlg.WindowTitle = PwDefs.ShortProductName;
					dlg.SetIcon(VtdCustomIcon.Question);
					dlg.AddButton((int)DialogResult.Yes, KPRes.YesCmd, null);
					dlg.AddButton((int)DialogResult.No, KPRes.NoCmd, null);
					dlg.AddButton((int)DialogResult.Cancel, KPRes.Cancel, null);
					dlg.DefaultButtonID = (int)DialogResult.Yes;

					DialogResult dr;
					if (dlg.ShowDialog(this)) dr = (DialogResult)dlg.Result;
					else dr = MessageService.Ask(strText, PwDefs.ShortProductName,
						MessageBoxButtons.YesNoCancel);

					if ((dr == DialogResult.Yes) || (dr == DialogResult.OK))
					{
						bCancel = !SaveEntry(m_pwEntry, true);
						if (!bCancel) DialogResult = DialogResult.OK;
					}
					else if ((dr == DialogResult.Cancel) || (dr == DialogResult.None))
						bCancel = true;
				}
			}

			if (bCancel)
			{
				e.Cancel = true;
				DialogResult = DialogResult.None;
			}
		}

		private void OnBinariesItemActivate(object sender, EventArgs e)
		{
			OnBtnBinOpen(sender, e);
		}

		private void OnHistoryItemActivate(object sender, EventArgs e)
		{
			OnBtnHistoryView(sender, e);
		}

		private void BinImportFiles(string[] vPaths)
		{
			if (vPaths == null) { Debug.Assert(false); return; }

			UpdateEntryBinaries(true, false);

			foreach (string strFile in vPaths)
			{
				if (string.IsNullOrEmpty(strFile)) { Debug.Assert(false); continue; }

				string strItem = UrlUtil.GetFileName(strFile);
				if (m_vBinaries.Get(strItem) != null)
				{
					string strMsg = KPRes.AttachedExistsAlready + MessageService.NewLine +
						strItem + MessageService.NewParagraph + KPRes.AttachNewRename +
						MessageService.NewParagraph + KPRes.AttachNewRenameRemarks0 +
						MessageService.NewLine + KPRes.AttachNewRenameRemarks1 +
						MessageService.NewLine + KPRes.AttachNewRenameRemarks2;
					DialogResult dr = MessageService.Ask(strMsg, null,
						MessageBoxButtons.YesNoCancel);

					if (dr == DialogResult.Cancel) continue;
					else if (dr == DialogResult.Yes)
					{
						string strFileName = UrlUtil.StripExtension(strItem);
						string strExtension = "." + UrlUtil.GetExtension(strItem);

						int nTry = 0;
						while (true)
						{
							string strNewName = strFileName + nTry.ToString() + strExtension;
							if (m_vBinaries.Get(strNewName) == null)
							{
								strItem = strNewName;
								break;
							}

							++nTry;
						}
					}
				}

				try
				{
					if (!FileDialogsEx.CheckAttachmentSize(strFile, KPRes.AttachFailed +
						MessageService.NewParagraph + strFile))
						continue;

					byte[] vBytes = File.ReadAllBytes(strFile);
					vBytes = DataEditorForm.ConvertAttachment(strItem, vBytes);

					if (vBytes != null)
					{
						ProtectedBinary pb = new ProtectedBinary(false, vBytes);
						m_vBinaries.Set(strItem, pb);
					}
				}
				catch (Exception exAttach)
				{
					MessageService.ShowWarning(KPRes.AttachFailed, strFile, exAttach);
				}
			}

			UpdateEntryBinaries(false, true);
			ResizeColumnHeaders();
		}

		private void OnBinAfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			string strNew = e.Label;
			e.CancelEdit = true; // In the case of success, we update it on our own

			if (string.IsNullOrEmpty(strNew)) return;

			int iItem = e.Item;
			if ((iItem < 0) || (iItem >= m_lvBinaries.Items.Count)) return;
			string strOld = m_lvBinaries.Items[iItem].Text;
			if (strNew == strOld) return;

			if (m_vBinaries.Get(strNew) != null)
			{
				MessageService.ShowWarning(KPRes.FieldNameExistsAlready);
				return;
			}

			ProtectedBinary pb = m_vBinaries.Get(strOld);
			if (pb == null) { Debug.Assert(false); return; }
			m_vBinaries.Remove(strOld);
			m_vBinaries.Set(strNew, pb);

			UpdateEntryBinaries(false, true, strNew);
		}

		private static void BinDragAccept(DragEventArgs e)
		{
			if (e == null) { Debug.Assert(false); return; }

			IDataObject ido = e.Data;
			if ((ido == null) || !ido.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.None;
			else e.Effect = DragDropEffects.Copy;
		}

		private void OnBinDragEnter(object sender, DragEventArgs e)
		{
			BinDragAccept(e);
		}

		private void OnBinDragOver(object sender, DragEventArgs e)
		{
			BinDragAccept(e);
		}

		private void OnBinDragDrop(object sender, DragEventArgs e)
		{
			try
			{
				BinImportFiles(e.Data.GetData(DataFormats.FileDrop) as string[]);
			}
			catch (Exception) { Debug.Assert(false); }
		}

		private void InitOverridesBox()
		{
			List<KeyValuePair<string, Image>> l = new List<KeyValuePair<string, Image>>();

			AddOverrideUrlItem(l, "cmd://{INTERNETEXPLORER} \"{URL}\"",
				AppLocator.InternetExplorerPath);
			AddOverrideUrlItem(l, "cmd://{INTERNETEXPLORER} -private \"{URL}\"",
				AppLocator.InternetExplorerPath);
			AddOverrideUrlItem(l, "microsoft-edge:{URL}",
				AppLocator.EdgePath);
			AddOverrideUrlItem(l, "cmd://{FIREFOX} \"{URL}\"",
				AppLocator.FirefoxPath);
			AddOverrideUrlItem(l, "cmd://{FIREFOX} -private-window \"{URL}\"",
				AppLocator.FirefoxPath);
			AddOverrideUrlItem(l, "cmd://{GOOGLECHROME} \"{URL}\"",
				AppLocator.ChromePath);
			AddOverrideUrlItem(l, "cmd://{GOOGLECHROME} --incognito \"{URL}\"",
				AppLocator.ChromePath);
			AddOverrideUrlItem(l, "cmd://{OPERA} \"{URL}\"",
				AppLocator.OperaPath);
			AddOverrideUrlItem(l, "cmd://{OPERA} --private \"{URL}\"",
				AppLocator.OperaPath);
			AddOverrideUrlItem(l, "cmd://{SAFARI} \"{URL}\"",
				AppLocator.SafariPath);
		}

		private void AddOverrideUrlItem(List<KeyValuePair<string, Image>> l,
			string strOverride, string strIconPath)
		{
			if (string.IsNullOrEmpty(strOverride)) { Debug.Assert(false); return; }

			int w = DpiUtil.ScaleIntX(16);
			int h = DpiUtil.ScaleIntY(16);

			Image img = null;
			string str = UrlUtil.GetQuotedAppPath(strIconPath ?? string.Empty);
			str = str.Trim();
			try
			{
				if ((str.Length > 0) && File.Exists(str))
				{
					// img = UIUtil.GetFileIcon(str, w, h);
					img = FileIcons.GetImageForPath(str, new Size(w, h), true, true);
				}
			}
			catch (Exception) { Debug.Assert(false); }

			if (img == null)
				img = GfxUtil.ScaleImage(m_ilIcons.Images[(int)PwIcon.Console],
					w, h, ScaleTransformFlags.UIIcon);

			l.Add(new KeyValuePair<string, Image>(strOverride, img));
		}

		private void OnCustomDataSelectedIndexChanged(object sender, EventArgs e)
		{
			EnableControlsEx();
		}

		private void OnBtnCDDel(object sender, EventArgs e)
		{
			UIUtil.StrDictListDeleteSel(m_lvCustomData, m_sdCustomData, (m_mvec != null));
			UIUtil.SetFocus(m_lvCustomData, this);
			EnableControlsEx();
		}

		private static void NormalizeStrings(ProtectedStringDictionary d,
			PwDatabase pd)
		{
			if (d == null) { Debug.Assert(false); return; }

			MemoryProtectionConfig mp = ((pd != null) ? pd.MemoryProtection :
				(new MemoryProtectionConfig()));

			string str = d.ReadSafe(PwDefs.NotesField);
			d.Set(PwDefs.NotesField, new ProtectedString(mp.ProtectNotes,
				StrUtil.NormalizeNewLines(str, true)));

			// Custom strings are normalized by the string editing form
		}

		private void InitUserNameSuggestions()
		{
			try
			{
				AceColumn c = Program.Config.MainWindow.FindColumn(
					AceColumnType.UserName);
				if ((c == null) || c.HideWithAsterisks) return;

				GFunc<PwEntry, string> f = delegate (PwEntry pe)
				{
					string str = pe.Strings.ReadSafe(PwDefs.UserNameField);
					return ((str.Length != 0) ? str : null);
				};

				string[] v = m_pwDatabase.RootGroup.CollectEntryStrings(f, true);

				// Do not append, because it breaks Ctrl+A;
				// https://sourceforge.net/p/keepass/discussion/329220/thread/4f626b91/
				UIUtil.EnableAutoCompletion(m_tbUserName, false, v); // Invokes
			}
			catch (Exception) { Debug.Assert(false); }
		}

		private void OnUrlTextChanged(object sender, EventArgs e)
		{
			EnableControlsEx(); // URL override warning
		}

		private void OnUrlOverrideTextChanged(object sender, EventArgs e)
		{
			EnableControlsEx(); // URL override warning
		}

		private void OnQualityCheckCheckedChanged(object sender, EventArgs e)
		{
			m_icgPassword.QualityEnabled = m_cbQualityCheck.Checked;
		}
	}
}
