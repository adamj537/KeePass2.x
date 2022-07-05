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

namespace KeePassLib.Interfaces
{
	public interface IUIOperations
	{
		/// <summary>
		/// Let the user interface save the current database.
		/// </summary>
		/// <param name="bForceSave">If <c>true</c>, the UI will not ask for
		/// whether to synchronize or overwrite, it'll simply overwrite the
		/// file.</param>
		/// <returns>Returns <c>true</c> if the file has been saved.</returns>
		bool UIFileSave(bool bForceSave);
	}
}
