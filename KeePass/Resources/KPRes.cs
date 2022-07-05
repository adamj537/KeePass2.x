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

using KeePass.App;

using KeePassLib.Resources;
using KeePassLib.Utility;

namespace KeePass.Resources
{
	public static partial class KPRes
	{
		// Obsoletes: for backward compatibility with plugins

		[Obsolete]
		public static string ChangeMasterKeyIntroShort
		{
			get { return string.Empty; }
		}

		[Obsolete]
		public static string CreateNewDatabase
		{
			get { return CreateNewDatabase2; }
		}

		public static string DeleteEntriesTitle
		{
			get { return StrUtil.CommandToText(DeleteEntriesCmd); }
		}

		public static string DeleteEntriesTitleSingle
		{
			get { return StrUtil.CommandToText(DeleteEntryCmd); }
		}

		[Obsolete]
		public static string EditCmd
		{
			get { return @"&Edit"; }
		}

		public static string EditEntries
		{
			get { return StrUtil.CommandToText(EditEntriesCmd); }
		}

		public static string EditEntry
		{
			get { return StrUtil.CommandToText(EditEntryCmd); }
		}

		[Obsolete]
		public static string GeneratedPasswordSamples
		{
			get { return GeneratedPasswords; }
		}

		[Obsolete]
		public static string NewDatabaseFileName
		{
			get { return (Database + "." + AppDefs.FileExtension.FileExt); }
		}

		[Obsolete]
		public static string ToolBarNew
		{
			get { return NewDatabase; }
		}

		[Obsolete]
		public static string ToolBarOpen
		{
			get { return OpenDatabase; }
		}

		[Obsolete]
		public static string ToolBarSaveAll
		{
			get { return SaveAllDatabases; }
		}

		[Obsolete]
		public static string UnknownError
		{
			get { return KLRes.UnknownError; }
		}
	}
}
