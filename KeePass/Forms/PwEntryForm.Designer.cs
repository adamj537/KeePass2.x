namespace KeePass.Forms
{
	partial class PwEntryForm
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.m_btnOK = new System.Windows.Forms.Button();
			this.m_btnCancel = new System.Windows.Forms.Button();
			this.m_ttRect = new System.Windows.Forms.ToolTip(this.components);
			this.m_lblSeparator = new System.Windows.Forms.Label();
			this.m_ttBalloon = new System.Windows.Forms.ToolTip(this.components);
			this.m_bannerImage = new System.Windows.Forms.PictureBox();
			this.m_tabHistory = new System.Windows.Forms.TabPage();
			this.m_lblPrev = new System.Windows.Forms.Label();
			this.m_lblModifiedData = new System.Windows.Forms.Label();
			this.m_lblModified = new System.Windows.Forms.Label();
			this.m_lblCreatedData = new System.Windows.Forms.Label();
			this.m_lblCreated = new System.Windows.Forms.Label();
			this.m_btnHistoryDelete = new System.Windows.Forms.Button();
			this.m_btnHistoryView = new System.Windows.Forms.Button();
			this.m_btnHistoryRestore = new System.Windows.Forms.Button();
			this.m_lvHistory = new System.Windows.Forms.ListView();
			this.m_tabProperties = new System.Windows.Forms.TabPage();
			this.m_btnTags = new System.Windows.Forms.Button();
			this.m_linkTagsInh = new System.Windows.Forms.LinkLabel();
			this.m_lblCustomData = new System.Windows.Forms.Label();
			this.m_btnCDDel = new System.Windows.Forms.Button();
			this.m_lvCustomData = new System.Windows.Forms.ListView();
			this.m_tbTags = new System.Windows.Forms.TextBox();
			this.m_tbUuid = new System.Windows.Forms.TextBox();
			this.m_lblTags = new System.Windows.Forms.Label();
			this.m_lblUuid = new System.Windows.Forms.Label();
			this.m_lblOverrideUrl = new System.Windows.Forms.Label();
			this.m_cmbOverrideUrl = new System.Windows.Forms.ComboBox();
			this.m_tabAdvanced = new System.Windows.Forms.TabPage();
			this.m_grpAttachments = new System.Windows.Forms.GroupBox();
			this.m_btnBinOpen = new System.Windows.Forms.Button();
			this.m_btnBinSave = new System.Windows.Forms.Button();
			this.m_btnBinDelete = new System.Windows.Forms.Button();
			this.m_btnBinAdd = new System.Windows.Forms.Button();
			this.m_lvBinaries = new System.Windows.Forms.ListView();
			this.m_grpStringFields = new System.Windows.Forms.GroupBox();
			this.m_btnStrAdd = new System.Windows.Forms.Button();
			this.m_btnStrEdit = new System.Windows.Forms.Button();
			this.m_btnStrDelete = new System.Windows.Forms.Button();
			this.m_lvStrings = new System.Windows.Forms.ListView();
			this.m_tabEntry = new System.Windows.Forms.TabPage();
			this.m_cbQualityCheck = new System.Windows.Forms.CheckBox();
			this.m_lblTitle = new System.Windows.Forms.Label();
			this.m_rtNotes = new System.Windows.Forms.RichTextBox();
			this.m_tbUrl = new System.Windows.Forms.TextBox();
			this.m_tbRepeatPassword = new System.Windows.Forms.TextBox();
			this.m_tbPassword = new System.Windows.Forms.TextBox();
			this.m_tbTitle = new System.Windows.Forms.TextBox();
			this.m_tbUserName = new System.Windows.Forms.TextBox();
			this.m_cbExpires = new System.Windows.Forms.CheckBox();
			this.m_lblQualityInfo = new System.Windows.Forms.Label();
			this.m_btnGenPw = new System.Windows.Forms.Button();
			this.m_cbHidePassword = new System.Windows.Forms.CheckBox();
			this.m_pbQuality = new System.Windows.Forms.ProgressBar();
			this.m_lblIcon = new System.Windows.Forms.Label();
			this.m_btnIcon = new System.Windows.Forms.Button();
			this.m_lblNotes = new System.Windows.Forms.Label();
			this.m_dtExpireDateTime = new System.Windows.Forms.DateTimePicker();
			this.m_lblUrl = new System.Windows.Forms.Label();
			this.m_lblQuality = new System.Windows.Forms.Label();
			this.m_lblPassword = new System.Windows.Forms.Label();
			this.m_lblPasswordRepeat = new System.Windows.Forms.Label();
			this.m_lblUserName = new System.Windows.Forms.Label();
			this.m_tabMain = new System.Windows.Forms.TabControl();
			((System.ComponentModel.ISupportInitialize)(this.m_bannerImage)).BeginInit();
			this.m_tabHistory.SuspendLayout();
			this.m_tabProperties.SuspendLayout();
			this.m_tabAdvanced.SuspendLayout();
			this.m_grpAttachments.SuspendLayout();
			this.m_grpStringFields.SuspendLayout();
			this.m_tabEntry.SuspendLayout();
			this.m_tabMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_btnOK
			// 
			this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.m_btnOK.Location = new System.Drawing.Point(313, 453);
			this.m_btnOK.Name = "m_btnOK";
			this.m_btnOK.Size = new System.Drawing.Size(80, 23);
			this.m_btnOK.TabIndex = 0;
			this.m_btnOK.Text = "OK";
			this.m_btnOK.UseVisualStyleBackColor = true;
			this.m_btnOK.Click += new System.EventHandler(this.OnBtnOK);
			// 
			// m_btnCancel
			// 
			this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_btnCancel.Location = new System.Drawing.Point(399, 453);
			this.m_btnCancel.Name = "m_btnCancel";
			this.m_btnCancel.Size = new System.Drawing.Size(80, 23);
			this.m_btnCancel.TabIndex = 1;
			this.m_btnCancel.Text = "Cancel";
			this.m_btnCancel.UseVisualStyleBackColor = true;
			this.m_btnCancel.Click += new System.EventHandler(this.OnBtnCancel);
			// 
			// m_lblSeparator
			// 
			this.m_lblSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_lblSeparator.Location = new System.Drawing.Point(0, 445);
			this.m_lblSeparator.Name = "m_lblSeparator";
			this.m_lblSeparator.Size = new System.Drawing.Size(489, 2);
			this.m_lblSeparator.TabIndex = 3;
			// 
			// m_ttBalloon
			// 
			this.m_ttBalloon.IsBalloon = true;
			// 
			// m_bannerImage
			// 
			this.m_bannerImage.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_bannerImage.Location = new System.Drawing.Point(0, 0);
			this.m_bannerImage.Name = "m_bannerImage";
			this.m_bannerImage.Size = new System.Drawing.Size(489, 60);
			this.m_bannerImage.TabIndex = 16;
			this.m_bannerImage.TabStop = false;
			// 
			// m_tabHistory
			// 
			this.m_tabHistory.Controls.Add(this.m_lblPrev);
			this.m_tabHistory.Controls.Add(this.m_lblModifiedData);
			this.m_tabHistory.Controls.Add(this.m_lblModified);
			this.m_tabHistory.Controls.Add(this.m_lblCreatedData);
			this.m_tabHistory.Controls.Add(this.m_lblCreated);
			this.m_tabHistory.Controls.Add(this.m_btnHistoryDelete);
			this.m_tabHistory.Controls.Add(this.m_btnHistoryView);
			this.m_tabHistory.Controls.Add(this.m_btnHistoryRestore);
			this.m_tabHistory.Controls.Add(this.m_lvHistory);
			this.m_tabHistory.Location = new System.Drawing.Point(4, 22);
			this.m_tabHistory.Name = "m_tabHistory";
			this.m_tabHistory.Size = new System.Drawing.Size(467, 342);
			this.m_tabHistory.TabIndex = 3;
			this.m_tabHistory.Text = "History";
			this.m_tabHistory.UseVisualStyleBackColor = true;
			// 
			// m_lblPrev
			// 
			this.m_lblPrev.AutoSize = true;
			this.m_lblPrev.Location = new System.Drawing.Point(6, 57);
			this.m_lblPrev.Name = "m_lblPrev";
			this.m_lblPrev.Size = new System.Drawing.Size(93, 13);
			this.m_lblPrev.TabIndex = 4;
			this.m_lblPrev.Text = "&Previous versions:";
			// 
			// m_lblModifiedData
			// 
			this.m_lblModifiedData.AutoSize = true;
			this.m_lblModifiedData.Location = new System.Drawing.Point(62, 35);
			this.m_lblModifiedData.Name = "m_lblModifiedData";
			this.m_lblModifiedData.Size = new System.Drawing.Size(19, 13);
			this.m_lblModifiedData.TabIndex = 3;
			this.m_lblModifiedData.Text = "<>";
			// 
			// m_lblModified
			// 
			this.m_lblModified.AutoSize = true;
			this.m_lblModified.Location = new System.Drawing.Point(6, 35);
			this.m_lblModified.Name = "m_lblModified";
			this.m_lblModified.Size = new System.Drawing.Size(50, 13);
			this.m_lblModified.TabIndex = 2;
			this.m_lblModified.Text = "Modified:";
			// 
			// m_lblCreatedData
			// 
			this.m_lblCreatedData.AutoSize = true;
			this.m_lblCreatedData.Location = new System.Drawing.Point(62, 13);
			this.m_lblCreatedData.Name = "m_lblCreatedData";
			this.m_lblCreatedData.Size = new System.Drawing.Size(19, 13);
			this.m_lblCreatedData.TabIndex = 1;
			this.m_lblCreatedData.Text = "<>";
			// 
			// m_lblCreated
			// 
			this.m_lblCreated.AutoSize = true;
			this.m_lblCreated.Location = new System.Drawing.Point(6, 13);
			this.m_lblCreated.Name = "m_lblCreated";
			this.m_lblCreated.Size = new System.Drawing.Size(47, 13);
			this.m_lblCreated.TabIndex = 0;
			this.m_lblCreated.Text = "Created:";
			// 
			// m_btnHistoryDelete
			// 
			this.m_btnHistoryDelete.Location = new System.Drawing.Point(89, 307);
			this.m_btnHistoryDelete.Name = "m_btnHistoryDelete";
			this.m_btnHistoryDelete.Size = new System.Drawing.Size(75, 23);
			this.m_btnHistoryDelete.TabIndex = 7;
			this.m_btnHistoryDelete.Text = "&Delete";
			this.m_btnHistoryDelete.UseVisualStyleBackColor = true;
			this.m_btnHistoryDelete.Click += new System.EventHandler(this.OnBtnHistoryDelete);
			// 
			// m_btnHistoryView
			// 
			this.m_btnHistoryView.Location = new System.Drawing.Point(8, 307);
			this.m_btnHistoryView.Name = "m_btnHistoryView";
			this.m_btnHistoryView.Size = new System.Drawing.Size(75, 23);
			this.m_btnHistoryView.TabIndex = 6;
			this.m_btnHistoryView.Text = "&View";
			this.m_btnHistoryView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.m_btnHistoryView.UseVisualStyleBackColor = true;
			this.m_btnHistoryView.Click += new System.EventHandler(this.OnBtnHistoryView);
			// 
			// m_btnHistoryRestore
			// 
			this.m_btnHistoryRestore.Location = new System.Drawing.Point(382, 307);
			this.m_btnHistoryRestore.Name = "m_btnHistoryRestore";
			this.m_btnHistoryRestore.Size = new System.Drawing.Size(75, 23);
			this.m_btnHistoryRestore.TabIndex = 8;
			this.m_btnHistoryRestore.Text = "&Restore";
			this.m_btnHistoryRestore.UseVisualStyleBackColor = true;
			this.m_btnHistoryRestore.Click += new System.EventHandler(this.OnBtnHistoryRestore);
			// 
			// m_lvHistory
			// 
			this.m_lvHistory.FullRowSelect = true;
			this.m_lvHistory.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.m_lvHistory.HideSelection = false;
			this.m_lvHistory.Location = new System.Drawing.Point(9, 73);
			this.m_lvHistory.Name = "m_lvHistory";
			this.m_lvHistory.ShowItemToolTips = true;
			this.m_lvHistory.Size = new System.Drawing.Size(447, 228);
			this.m_lvHistory.TabIndex = 5;
			this.m_lvHistory.UseCompatibleStateImageBehavior = false;
			this.m_lvHistory.View = System.Windows.Forms.View.Details;
			this.m_lvHistory.ItemActivate += new System.EventHandler(this.OnHistoryItemActivate);
			this.m_lvHistory.SelectedIndexChanged += new System.EventHandler(this.OnHistorySelectedIndexChanged);
			// 
			// m_tabProperties
			// 
			this.m_tabProperties.Controls.Add(this.m_btnTags);
			this.m_tabProperties.Controls.Add(this.m_linkTagsInh);
			this.m_tabProperties.Controls.Add(this.m_lblCustomData);
			this.m_tabProperties.Controls.Add(this.m_btnCDDel);
			this.m_tabProperties.Controls.Add(this.m_lvCustomData);
			this.m_tabProperties.Controls.Add(this.m_tbTags);
			this.m_tabProperties.Controls.Add(this.m_tbUuid);
			this.m_tabProperties.Controls.Add(this.m_lblTags);
			this.m_tabProperties.Controls.Add(this.m_lblUuid);
			this.m_tabProperties.Controls.Add(this.m_lblOverrideUrl);
			this.m_tabProperties.Controls.Add(this.m_cmbOverrideUrl);
			this.m_tabProperties.Location = new System.Drawing.Point(4, 22);
			this.m_tabProperties.Name = "m_tabProperties";
			this.m_tabProperties.Size = new System.Drawing.Size(467, 342);
			this.m_tabProperties.TabIndex = 4;
			this.m_tabProperties.Text = "Properties";
			this.m_tabProperties.UseVisualStyleBackColor = true;
			// 
			// m_btnTags
			// 
			this.m_btnTags.Location = new System.Drawing.Point(424, 90);
			this.m_btnTags.Name = "m_btnTags";
			this.m_btnTags.Size = new System.Drawing.Size(32, 23);
			this.m_btnTags.TabIndex = 7;
			this.m_btnTags.UseVisualStyleBackColor = true;
			// 
			// m_linkTagsInh
			// 
			this.m_linkTagsInh.Location = new System.Drawing.Point(199, 74);
			this.m_linkTagsInh.Name = "m_linkTagsInh";
			this.m_linkTagsInh.Size = new System.Drawing.Size(260, 14);
			this.m_linkTagsInh.TabIndex = 4;
			this.m_linkTagsInh.TabStop = true;
			this.m_linkTagsInh.Text = "<DYN>";
			this.m_linkTagsInh.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// m_lblCustomData
			// 
			this.m_lblCustomData.AutoSize = true;
			this.m_lblCustomData.Location = new System.Drawing.Point(6, 175);
			this.m_lblCustomData.Name = "m_lblCustomData";
			this.m_lblCustomData.Size = new System.Drawing.Size(63, 13);
			this.m_lblCustomData.TabIndex = 10;
			this.m_lblCustomData.Text = "&Plugin data:";
			// 
			// m_btnCDDel
			// 
			this.m_btnCDDel.Location = new System.Drawing.Point(381, 192);
			this.m_btnCDDel.Name = "m_btnCDDel";
			this.m_btnCDDel.Size = new System.Drawing.Size(75, 23);
			this.m_btnCDDel.TabIndex = 12;
			this.m_btnCDDel.Text = "&Delete";
			this.m_btnCDDel.UseVisualStyleBackColor = true;
			this.m_btnCDDel.Click += new System.EventHandler(this.OnBtnCDDel);
			// 
			// m_lvCustomData
			// 
			this.m_lvCustomData.FullRowSelect = true;
			this.m_lvCustomData.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.m_lvCustomData.HideSelection = false;
			this.m_lvCustomData.Location = new System.Drawing.Point(9, 193);
			this.m_lvCustomData.Name = "m_lvCustomData";
			this.m_lvCustomData.ShowItemToolTips = true;
			this.m_lvCustomData.Size = new System.Drawing.Size(366, 101);
			this.m_lvCustomData.TabIndex = 11;
			this.m_lvCustomData.UseCompatibleStateImageBehavior = false;
			this.m_lvCustomData.View = System.Windows.Forms.View.Details;
			this.m_lvCustomData.SelectedIndexChanged += new System.EventHandler(this.OnCustomDataSelectedIndexChanged);
			// 
			// m_tbTags
			// 
			this.m_tbTags.Location = new System.Drawing.Point(9, 92);
			this.m_tbTags.Name = "m_tbTags";
			this.m_tbTags.Size = new System.Drawing.Size(409, 20);
			this.m_tbTags.TabIndex = 6;
			// 
			// m_tbUuid
			// 
			this.m_tbUuid.Location = new System.Drawing.Point(49, 309);
			this.m_tbUuid.Name = "m_tbUuid";
			this.m_tbUuid.ReadOnly = true;
			this.m_tbUuid.Size = new System.Drawing.Size(407, 20);
			this.m_tbUuid.TabIndex = 14;
			// 
			// m_lblTags
			// 
			this.m_lblTags.AutoSize = true;
			this.m_lblTags.Location = new System.Drawing.Point(6, 74);
			this.m_lblTags.Name = "m_lblTags";
			this.m_lblTags.Size = new System.Drawing.Size(34, 13);
			this.m_lblTags.TabIndex = 5;
			this.m_lblTags.Text = "&Tags:";
			// 
			// m_lblUuid
			// 
			this.m_lblUuid.AutoSize = true;
			this.m_lblUuid.Location = new System.Drawing.Point(6, 312);
			this.m_lblUuid.Name = "m_lblUuid";
			this.m_lblUuid.Size = new System.Drawing.Size(37, 13);
			this.m_lblUuid.TabIndex = 13;
			this.m_lblUuid.Text = "&UUID:";
			// 
			// m_lblOverrideUrl
			// 
			this.m_lblOverrideUrl.AutoSize = true;
			this.m_lblOverrideUrl.Location = new System.Drawing.Point(6, 124);
			this.m_lblOverrideUrl.Name = "m_lblOverrideUrl";
			this.m_lblOverrideUrl.Size = new System.Drawing.Size(222, 13);
			this.m_lblOverrideUrl.TabIndex = 8;
			this.m_lblOverrideUrl.Text = "O&verride URL (e.g. to use a specific browser):";
			// 
			// m_cmbOverrideUrl
			// 
			this.m_cmbOverrideUrl.IntegralHeight = false;
			this.m_cmbOverrideUrl.Location = new System.Drawing.Point(9, 142);
			this.m_cmbOverrideUrl.MaxDropDownItems = 16;
			this.m_cmbOverrideUrl.Name = "m_cmbOverrideUrl";
			this.m_cmbOverrideUrl.Size = new System.Drawing.Size(447, 21);
			this.m_cmbOverrideUrl.TabIndex = 9;
			this.m_cmbOverrideUrl.TextChanged += new System.EventHandler(this.OnUrlOverrideTextChanged);
			// 
			// m_tabAdvanced
			// 
			this.m_tabAdvanced.Controls.Add(this.m_grpAttachments);
			this.m_tabAdvanced.Controls.Add(this.m_grpStringFields);
			this.m_tabAdvanced.Location = new System.Drawing.Point(4, 22);
			this.m_tabAdvanced.Name = "m_tabAdvanced";
			this.m_tabAdvanced.Padding = new System.Windows.Forms.Padding(3);
			this.m_tabAdvanced.Size = new System.Drawing.Size(467, 342);
			this.m_tabAdvanced.TabIndex = 1;
			this.m_tabAdvanced.Text = "Advanced";
			this.m_tabAdvanced.UseVisualStyleBackColor = true;
			// 
			// m_grpAttachments
			// 
			this.m_grpAttachments.Controls.Add(this.m_btnBinOpen);
			this.m_grpAttachments.Controls.Add(this.m_btnBinSave);
			this.m_grpAttachments.Controls.Add(this.m_btnBinDelete);
			this.m_grpAttachments.Controls.Add(this.m_btnBinAdd);
			this.m_grpAttachments.Controls.Add(this.m_lvBinaries);
			this.m_grpAttachments.Location = new System.Drawing.Point(6, 174);
			this.m_grpAttachments.Name = "m_grpAttachments";
			this.m_grpAttachments.Size = new System.Drawing.Size(455, 162);
			this.m_grpAttachments.TabIndex = 1;
			this.m_grpAttachments.TabStop = false;
			this.m_grpAttachments.Text = "File attachments";
			// 
			// m_btnBinOpen
			// 
			this.m_btnBinOpen.Location = new System.Drawing.Point(374, 104);
			this.m_btnBinOpen.Name = "m_btnBinOpen";
			this.m_btnBinOpen.Size = new System.Drawing.Size(75, 23);
			this.m_btnBinOpen.TabIndex = 3;
			this.m_btnBinOpen.Text = "Ope&n";
			this.m_btnBinOpen.UseVisualStyleBackColor = true;
			this.m_btnBinOpen.Click += new System.EventHandler(this.OnBtnBinOpen);
			// 
			// m_btnBinSave
			// 
			this.m_btnBinSave.Location = new System.Drawing.Point(374, 133);
			this.m_btnBinSave.Name = "m_btnBinSave";
			this.m_btnBinSave.Size = new System.Drawing.Size(75, 23);
			this.m_btnBinSave.TabIndex = 4;
			this.m_btnBinSave.Text = "&Save";
			this.m_btnBinSave.UseVisualStyleBackColor = true;
			this.m_btnBinSave.Click += new System.EventHandler(this.OnBtnBinSave);
			// 
			// m_btnBinDelete
			// 
			this.m_btnBinDelete.Location = new System.Drawing.Point(374, 48);
			this.m_btnBinDelete.Name = "m_btnBinDelete";
			this.m_btnBinDelete.Size = new System.Drawing.Size(75, 23);
			this.m_btnBinDelete.TabIndex = 2;
			this.m_btnBinDelete.Text = "De&lete";
			this.m_btnBinDelete.UseVisualStyleBackColor = true;
			this.m_btnBinDelete.Click += new System.EventHandler(this.OnBtnBinDelete);
			// 
			// m_btnBinAdd
			// 
			this.m_btnBinAdd.Location = new System.Drawing.Point(374, 19);
			this.m_btnBinAdd.Name = "m_btnBinAdd";
			this.m_btnBinAdd.Size = new System.Drawing.Size(75, 23);
			this.m_btnBinAdd.TabIndex = 1;
			this.m_btnBinAdd.Text = "A&ttach";
			this.m_btnBinAdd.UseVisualStyleBackColor = true;
			this.m_btnBinAdd.Click += new System.EventHandler(this.OnBtnBinAdd);
			// 
			// m_lvBinaries
			// 
			this.m_lvBinaries.AllowDrop = true;
			this.m_lvBinaries.FullRowSelect = true;
			this.m_lvBinaries.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.m_lvBinaries.HideSelection = false;
			this.m_lvBinaries.LabelEdit = true;
			this.m_lvBinaries.Location = new System.Drawing.Point(6, 20);
			this.m_lvBinaries.Name = "m_lvBinaries";
			this.m_lvBinaries.ShowItemToolTips = true;
			this.m_lvBinaries.Size = new System.Drawing.Size(362, 135);
			this.m_lvBinaries.TabIndex = 0;
			this.m_lvBinaries.UseCompatibleStateImageBehavior = false;
			this.m_lvBinaries.View = System.Windows.Forms.View.Details;
			this.m_lvBinaries.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.OnBinAfterLabelEdit);
			this.m_lvBinaries.ItemActivate += new System.EventHandler(this.OnBinariesItemActivate);
			this.m_lvBinaries.SelectedIndexChanged += new System.EventHandler(this.OnBinariesSelectedIndexChanged);
			this.m_lvBinaries.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnBinDragDrop);
			this.m_lvBinaries.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnBinDragEnter);
			this.m_lvBinaries.DragOver += new System.Windows.Forms.DragEventHandler(this.OnBinDragOver);
			// 
			// m_grpStringFields
			// 
			this.m_grpStringFields.Controls.Add(this.m_btnStrAdd);
			this.m_grpStringFields.Controls.Add(this.m_btnStrEdit);
			this.m_grpStringFields.Controls.Add(this.m_btnStrDelete);
			this.m_grpStringFields.Controls.Add(this.m_lvStrings);
			this.m_grpStringFields.Location = new System.Drawing.Point(6, 6);
			this.m_grpStringFields.Name = "m_grpStringFields";
			this.m_grpStringFields.Size = new System.Drawing.Size(455, 162);
			this.m_grpStringFields.TabIndex = 0;
			this.m_grpStringFields.TabStop = false;
			this.m_grpStringFields.Text = "String fields";
			// 
			// m_btnStrAdd
			// 
			this.m_btnStrAdd.Location = new System.Drawing.Point(374, 19);
			this.m_btnStrAdd.Name = "m_btnStrAdd";
			this.m_btnStrAdd.Size = new System.Drawing.Size(75, 23);
			this.m_btnStrAdd.TabIndex = 1;
			this.m_btnStrAdd.Text = "&Add";
			this.m_btnStrAdd.UseVisualStyleBackColor = true;
			this.m_btnStrAdd.Click += new System.EventHandler(this.OnBtnStrAdd);
			// 
			// m_btnStrEdit
			// 
			this.m_btnStrEdit.Location = new System.Drawing.Point(374, 48);
			this.m_btnStrEdit.Name = "m_btnStrEdit";
			this.m_btnStrEdit.Size = new System.Drawing.Size(75, 23);
			this.m_btnStrEdit.TabIndex = 2;
			this.m_btnStrEdit.Text = "&Edit";
			this.m_btnStrEdit.UseVisualStyleBackColor = true;
			this.m_btnStrEdit.Click += new System.EventHandler(this.OnBtnStrEdit);
			// 
			// m_btnStrDelete
			// 
			this.m_btnStrDelete.Location = new System.Drawing.Point(374, 77);
			this.m_btnStrDelete.Name = "m_btnStrDelete";
			this.m_btnStrDelete.Size = new System.Drawing.Size(75, 23);
			this.m_btnStrDelete.TabIndex = 3;
			this.m_btnStrDelete.Text = "&Delete";
			this.m_btnStrDelete.UseVisualStyleBackColor = true;
			this.m_btnStrDelete.Click += new System.EventHandler(this.OnBtnStrDelete);
			// 
			// m_lvStrings
			// 
			this.m_lvStrings.FullRowSelect = true;
			this.m_lvStrings.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.m_lvStrings.HideSelection = false;
			this.m_lvStrings.Location = new System.Drawing.Point(6, 20);
			this.m_lvStrings.Name = "m_lvStrings";
			this.m_lvStrings.ShowItemToolTips = true;
			this.m_lvStrings.Size = new System.Drawing.Size(362, 135);
			this.m_lvStrings.TabIndex = 0;
			this.m_lvStrings.UseCompatibleStateImageBehavior = false;
			this.m_lvStrings.View = System.Windows.Forms.View.Details;
			this.m_lvStrings.ItemActivate += new System.EventHandler(this.OnStringsItemActivate);
			this.m_lvStrings.SelectedIndexChanged += new System.EventHandler(this.OnStringsSelectedIndexChanged);
			// 
			// m_tabEntry
			// 
			this.m_tabEntry.Controls.Add(this.m_cbQualityCheck);
			this.m_tabEntry.Controls.Add(this.m_lblTitle);
			this.m_tabEntry.Controls.Add(this.m_rtNotes);
			this.m_tabEntry.Controls.Add(this.m_tbUrl);
			this.m_tabEntry.Controls.Add(this.m_tbRepeatPassword);
			this.m_tabEntry.Controls.Add(this.m_tbPassword);
			this.m_tabEntry.Controls.Add(this.m_tbTitle);
			this.m_tabEntry.Controls.Add(this.m_tbUserName);
			this.m_tabEntry.Controls.Add(this.m_cbExpires);
			this.m_tabEntry.Controls.Add(this.m_lblQualityInfo);
			this.m_tabEntry.Controls.Add(this.m_btnGenPw);
			this.m_tabEntry.Controls.Add(this.m_cbHidePassword);
			this.m_tabEntry.Controls.Add(this.m_pbQuality);
			this.m_tabEntry.Controls.Add(this.m_lblIcon);
			this.m_tabEntry.Controls.Add(this.m_btnIcon);
			this.m_tabEntry.Controls.Add(this.m_lblNotes);
			this.m_tabEntry.Controls.Add(this.m_dtExpireDateTime);
			this.m_tabEntry.Controls.Add(this.m_lblUrl);
			this.m_tabEntry.Controls.Add(this.m_lblQuality);
			this.m_tabEntry.Controls.Add(this.m_lblPassword);
			this.m_tabEntry.Controls.Add(this.m_lblPasswordRepeat);
			this.m_tabEntry.Controls.Add(this.m_lblUserName);
			this.m_tabEntry.Location = new System.Drawing.Point(4, 22);
			this.m_tabEntry.Name = "m_tabEntry";
			this.m_tabEntry.Padding = new System.Windows.Forms.Padding(3);
			this.m_tabEntry.Size = new System.Drawing.Size(467, 342);
			this.m_tabEntry.TabIndex = 0;
			this.m_tabEntry.Text = "General";
			this.m_tabEntry.UseVisualStyleBackColor = true;
			// 
			// m_cbQualityCheck
			// 
			this.m_cbQualityCheck.Appearance = System.Windows.Forms.Appearance.Button;
			this.m_cbQualityCheck.Location = new System.Drawing.Point(424, 116);
			this.m_cbQualityCheck.Name = "m_cbQualityCheck";
			this.m_cbQualityCheck.Size = new System.Drawing.Size(32, 20);
			this.m_cbQualityCheck.TabIndex = 15;
			this.m_cbQualityCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.m_cbQualityCheck.UseVisualStyleBackColor = true;
			this.m_cbQualityCheck.CheckedChanged += new System.EventHandler(this.OnQualityCheckCheckedChanged);
			// 
			// m_lblTitle
			// 
			this.m_lblTitle.AutoSize = true;
			this.m_lblTitle.Location = new System.Drawing.Point(6, 13);
			this.m_lblTitle.Name = "m_lblTitle";
			this.m_lblTitle.Size = new System.Drawing.Size(30, 13);
			this.m_lblTitle.TabIndex = 0;
			this.m_lblTitle.Text = "&Title:";
			// 
			// m_rtNotes
			// 
			this.m_rtNotes.Location = new System.Drawing.Point(81, 168);
			this.m_rtNotes.Name = "m_rtNotes";
			this.m_rtNotes.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.m_rtNotes.Size = new System.Drawing.Size(375, 139);
			this.m_rtNotes.TabIndex = 19;
			this.m_rtNotes.Text = "";
			// 
			// m_tbUrl
			// 
			this.m_tbUrl.Location = new System.Drawing.Point(81, 141);
			this.m_tbUrl.Name = "m_tbUrl";
			this.m_tbUrl.Size = new System.Drawing.Size(374, 20);
			this.m_tbUrl.TabIndex = 17;
			this.m_tbUrl.TextChanged += new System.EventHandler(this.OnUrlTextChanged);
			// 
			// m_tbRepeatPassword
			// 
			this.m_tbRepeatPassword.Location = new System.Drawing.Point(81, 91);
			this.m_tbRepeatPassword.Name = "m_tbRepeatPassword";
			this.m_tbRepeatPassword.Size = new System.Drawing.Size(337, 20);
			this.m_tbRepeatPassword.TabIndex = 10;
			// 
			// m_tbPassword
			// 
			this.m_tbPassword.Location = new System.Drawing.Point(81, 64);
			this.m_tbPassword.Name = "m_tbPassword";
			this.m_tbPassword.Size = new System.Drawing.Size(337, 20);
			this.m_tbPassword.TabIndex = 7;
			// 
			// m_tbTitle
			// 
			this.m_tbTitle.Location = new System.Drawing.Point(81, 10);
			this.m_tbTitle.Name = "m_tbTitle";
			this.m_tbTitle.Size = new System.Drawing.Size(294, 20);
			this.m_tbTitle.TabIndex = 1;
			// 
			// m_tbUserName
			// 
			this.m_tbUserName.Location = new System.Drawing.Point(81, 37);
			this.m_tbUserName.Name = "m_tbUserName";
			this.m_tbUserName.Size = new System.Drawing.Size(374, 20);
			this.m_tbUserName.TabIndex = 5;
			// 
			// m_cbExpires
			// 
			this.m_cbExpires.AutoSize = true;
			this.m_cbExpires.Location = new System.Drawing.Point(9, 315);
			this.m_cbExpires.Name = "m_cbExpires";
			this.m_cbExpires.Size = new System.Drawing.Size(63, 17);
			this.m_cbExpires.TabIndex = 20;
			this.m_cbExpires.Text = "&Expires:";
			this.m_cbExpires.UseVisualStyleBackColor = true;
			// 
			// m_lblQualityInfo
			// 
			this.m_lblQualityInfo.Location = new System.Drawing.Point(371, 119);
			this.m_lblQualityInfo.Name = "m_lblQualityInfo";
			this.m_lblQualityInfo.Size = new System.Drawing.Size(50, 13);
			this.m_lblQualityInfo.TabIndex = 14;
			this.m_lblQualityInfo.Text = "0 ch.";
			this.m_lblQualityInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// m_btnGenPw
			// 
			this.m_btnGenPw.Location = new System.Drawing.Point(424, 89);
			this.m_btnGenPw.Name = "m_btnGenPw";
			this.m_btnGenPw.Size = new System.Drawing.Size(32, 23);
			this.m_btnGenPw.TabIndex = 11;
			this.m_btnGenPw.UseVisualStyleBackColor = true;
			// 
			// m_cbHidePassword
			// 
			this.m_cbHidePassword.Appearance = System.Windows.Forms.Appearance.Button;
			this.m_cbHidePassword.Location = new System.Drawing.Point(424, 62);
			this.m_cbHidePassword.Name = "m_cbHidePassword";
			this.m_cbHidePassword.Size = new System.Drawing.Size(32, 23);
			this.m_cbHidePassword.TabIndex = 8;
			this.m_cbHidePassword.Text = "***";
			this.m_cbHidePassword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.m_cbHidePassword.UseVisualStyleBackColor = true;
			// 
			// m_pbQuality
			// 
			this.m_pbQuality.Location = new System.Drawing.Point(81, 118);
			this.m_pbQuality.Name = "m_pbQuality";
			this.m_pbQuality.Size = new System.Drawing.Size(287, 16);
			this.m_pbQuality.TabIndex = 13;
			this.m_pbQuality.TabStop = false;
			// 
			// m_lblIcon
			// 
			this.m_lblIcon.AutoSize = true;
			this.m_lblIcon.Location = new System.Drawing.Point(387, 13);
			this.m_lblIcon.Name = "m_lblIcon";
			this.m_lblIcon.Size = new System.Drawing.Size(31, 13);
			this.m_lblIcon.TabIndex = 2;
			this.m_lblIcon.Text = "&Icon:";
			// 
			// m_btnIcon
			// 
			this.m_btnIcon.Location = new System.Drawing.Point(424, 8);
			this.m_btnIcon.Name = "m_btnIcon";
			this.m_btnIcon.Size = new System.Drawing.Size(32, 23);
			this.m_btnIcon.TabIndex = 3;
			this.m_btnIcon.UseVisualStyleBackColor = true;
			this.m_btnIcon.Click += new System.EventHandler(this.OnBtnPickIcon);
			// 
			// m_lblNotes
			// 
			this.m_lblNotes.AutoSize = true;
			this.m_lblNotes.Location = new System.Drawing.Point(6, 171);
			this.m_lblNotes.Name = "m_lblNotes";
			this.m_lblNotes.Size = new System.Drawing.Size(38, 13);
			this.m_lblNotes.TabIndex = 18;
			this.m_lblNotes.Text = "&Notes:";
			// 
			// m_dtExpireDateTime
			// 
			this.m_dtExpireDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.m_dtExpireDateTime.Location = new System.Drawing.Point(81, 313);
			this.m_dtExpireDateTime.Name = "m_dtExpireDateTime";
			this.m_dtExpireDateTime.Size = new System.Drawing.Size(374, 20);
			this.m_dtExpireDateTime.TabIndex = 21;
			// 
			// m_lblUrl
			// 
			this.m_lblUrl.AutoSize = true;
			this.m_lblUrl.Location = new System.Drawing.Point(6, 144);
			this.m_lblUrl.Name = "m_lblUrl";
			this.m_lblUrl.Size = new System.Drawing.Size(32, 13);
			this.m_lblUrl.TabIndex = 16;
			this.m_lblUrl.Text = "UR&L:";
			// 
			// m_lblQuality
			// 
			this.m_lblQuality.AutoSize = true;
			this.m_lblQuality.Location = new System.Drawing.Point(6, 119);
			this.m_lblQuality.Name = "m_lblQuality";
			this.m_lblQuality.Size = new System.Drawing.Size(42, 13);
			this.m_lblQuality.TabIndex = 12;
			this.m_lblQuality.Text = "Quality:";
			// 
			// m_lblPassword
			// 
			this.m_lblPassword.AutoSize = true;
			this.m_lblPassword.Location = new System.Drawing.Point(6, 67);
			this.m_lblPassword.Name = "m_lblPassword";
			this.m_lblPassword.Size = new System.Drawing.Size(56, 13);
			this.m_lblPassword.TabIndex = 6;
			this.m_lblPassword.Text = "&Password:";
			// 
			// m_lblPasswordRepeat
			// 
			this.m_lblPasswordRepeat.AutoSize = true;
			this.m_lblPasswordRepeat.Location = new System.Drawing.Point(6, 94);
			this.m_lblPasswordRepeat.Name = "m_lblPasswordRepeat";
			this.m_lblPasswordRepeat.Size = new System.Drawing.Size(45, 13);
			this.m_lblPasswordRepeat.TabIndex = 9;
			this.m_lblPasswordRepeat.Text = "&Repeat:";
			// 
			// m_lblUserName
			// 
			this.m_lblUserName.AutoSize = true;
			this.m_lblUserName.Location = new System.Drawing.Point(6, 40);
			this.m_lblUserName.Name = "m_lblUserName";
			this.m_lblUserName.Size = new System.Drawing.Size(61, 13);
			this.m_lblUserName.TabIndex = 4;
			this.m_lblUserName.Text = "&User name:";
			// 
			// m_tabMain
			// 
			this.m_tabMain.Controls.Add(this.m_tabEntry);
			this.m_tabMain.Controls.Add(this.m_tabAdvanced);
			this.m_tabMain.Controls.Add(this.m_tabProperties);
			this.m_tabMain.Controls.Add(this.m_tabHistory);
			this.m_tabMain.Location = new System.Drawing.Point(8, 66);
			this.m_tabMain.Name = "m_tabMain";
			this.m_tabMain.SelectedIndex = 0;
			this.m_tabMain.Size = new System.Drawing.Size(475, 368);
			this.m_tabMain.TabIndex = 2;
			// 
			// PwEntryForm
			// 
			this.AcceptButton = this.m_btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.m_btnCancel;
			this.ClientSize = new System.Drawing.Size(489, 486);
			this.Controls.Add(this.m_tabMain);
			this.Controls.Add(this.m_lblSeparator);
			this.Controls.Add(this.m_bannerImage);
			this.Controls.Add(this.m_btnCancel);
			this.Controls.Add(this.m_btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PwEntryForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "<DYN>";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.m_bannerImage)).EndInit();
			this.m_tabHistory.ResumeLayout(false);
			this.m_tabHistory.PerformLayout();
			this.m_tabProperties.ResumeLayout(false);
			this.m_tabProperties.PerformLayout();
			this.m_tabAdvanced.ResumeLayout(false);
			this.m_grpAttachments.ResumeLayout(false);
			this.m_grpStringFields.ResumeLayout(false);
			this.m_tabEntry.ResumeLayout(false);
			this.m_tabEntry.PerformLayout();
			this.m_tabMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button m_btnOK;
		private System.Windows.Forms.Button m_btnCancel;
		private System.Windows.Forms.PictureBox m_bannerImage;
		private System.Windows.Forms.ToolTip m_ttRect;
		private System.Windows.Forms.Label m_lblSeparator;
		private System.Windows.Forms.ToolTip m_ttBalloon;
		private System.Windows.Forms.TabPage m_tabHistory;
		private System.Windows.Forms.Label m_lblPrev;
		private System.Windows.Forms.Label m_lblModifiedData;
		private System.Windows.Forms.Label m_lblModified;
		private System.Windows.Forms.Label m_lblCreatedData;
		private System.Windows.Forms.Label m_lblCreated;
		private System.Windows.Forms.Button m_btnHistoryDelete;
		private System.Windows.Forms.Button m_btnHistoryView;
		private System.Windows.Forms.Button m_btnHistoryRestore;
		private System.Windows.Forms.ListView m_lvHistory;
		private System.Windows.Forms.TabPage m_tabProperties;
		private System.Windows.Forms.Button m_btnTags;
		private System.Windows.Forms.LinkLabel m_linkTagsInh;
		private System.Windows.Forms.Label m_lblCustomData;
		private System.Windows.Forms.Button m_btnCDDel;
		private System.Windows.Forms.ListView m_lvCustomData;
		private System.Windows.Forms.TextBox m_tbTags;
		private System.Windows.Forms.TextBox m_tbUuid;
		private System.Windows.Forms.Label m_lblTags;
		private System.Windows.Forms.Label m_lblUuid;
		private System.Windows.Forms.Label m_lblOverrideUrl;
		private System.Windows.Forms.ComboBox m_cmbOverrideUrl;
		private System.Windows.Forms.TabPage m_tabAdvanced;
		private System.Windows.Forms.GroupBox m_grpAttachments;
		private System.Windows.Forms.Button m_btnBinOpen;
		private System.Windows.Forms.Button m_btnBinSave;
		private System.Windows.Forms.Button m_btnBinDelete;
		private System.Windows.Forms.Button m_btnBinAdd;
		private System.Windows.Forms.ListView m_lvBinaries;
		private System.Windows.Forms.GroupBox m_grpStringFields;
		private System.Windows.Forms.Button m_btnStrAdd;
		private System.Windows.Forms.Button m_btnStrEdit;
		private System.Windows.Forms.Button m_btnStrDelete;
		private System.Windows.Forms.ListView m_lvStrings;
		private System.Windows.Forms.TabPage m_tabEntry;
		private System.Windows.Forms.CheckBox m_cbQualityCheck;
		private System.Windows.Forms.Label m_lblTitle;
		private System.Windows.Forms.RichTextBox m_rtNotes;
		private System.Windows.Forms.TextBox m_tbUrl;
		private System.Windows.Forms.TextBox m_tbRepeatPassword;
		private System.Windows.Forms.TextBox m_tbPassword;
		private System.Windows.Forms.TextBox m_tbTitle;
		private System.Windows.Forms.TextBox m_tbUserName;
		private System.Windows.Forms.CheckBox m_cbExpires;
		private System.Windows.Forms.Label m_lblQualityInfo;
		private System.Windows.Forms.Button m_btnGenPw;
		private System.Windows.Forms.CheckBox m_cbHidePassword;
		private System.Windows.Forms.ProgressBar m_pbQuality;
		private System.Windows.Forms.Label m_lblIcon;
		private System.Windows.Forms.Button m_btnIcon;
		private System.Windows.Forms.Label m_lblNotes;
		private System.Windows.Forms.DateTimePicker m_dtExpireDateTime;
		private System.Windows.Forms.Label m_lblUrl;
		private System.Windows.Forms.Label m_lblQuality;
		private System.Windows.Forms.Label m_lblPassword;
		private System.Windows.Forms.Label m_lblPasswordRepeat;
		private System.Windows.Forms.Label m_lblUserName;
		private System.Windows.Forms.TabControl m_tabMain;
	}
}