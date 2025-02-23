﻿/*
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

using System.IO;
using System.Windows.Forms;

using KeePass.Forms;
using KeePass.Resources;
using KeePass.UI;
using KeePassLib.Interfaces;
using KeePassLib.Utility;

namespace KeePass.DataExchange.Formats
{
	internal sealed class KeePassHtml2x : FileFormatProvider
	{
		public override bool SupportsImport { get { return false; } }
		public override bool SupportsExport { get { return true; } }

		public override string FormatName { get { return KPRes.CustomizableHtml; } }
		public override string DefaultExtension { get { return "html|htm"; } }
		public override string ApplicationGroup { get { return KPRes.General; } }

		public override bool Export(PwExportInfo pwExportInfo, Stream sOutput,
			IStatusLogger slLogger)
		{
			ImageList il = null;
			MainForm mf = Program.MainForm;
			if ((mf != null) && (mf.ActiveDatabase == pwExportInfo.ContextDatabase))
				il = mf.ClientIcons;

			PrintForm dlg = new PrintForm();
			dlg.InitEx(pwExportInfo.DataGroup, pwExportInfo.ContextDatabase, il,
				false, -1);

			bool bResult = false;
			try
			{
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					byte[] pb = StrUtil.Utf8.GetBytes(dlg.GeneratedHtml);
					sOutput.Write(pb, 0, pb.Length);
					sOutput.Close();

					bResult = true;
				}
			}
			finally { UIUtil.DestroyForm(dlg); }

			return bResult;
		}
	}
}
