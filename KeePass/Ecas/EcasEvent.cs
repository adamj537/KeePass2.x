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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;

using KeePassLib;
using KeePassLib.Interfaces;

namespace KeePass.Ecas
{
	public sealed class EcasEvent : IDeepCloneable<EcasEvent>, IEcasObject
	{
		private PwUuid m_type = PwUuid.Zero;
		[XmlIgnore]
		public PwUuid Type
		{
			get { return m_type; }
			set
			{
				if (value == null) throw new ArgumentNullException("value");
				m_type = value;
			}
		}

		[XmlElement("TypeGuid")]
		public string TypeString
		{
			get { return Convert.ToBase64String(m_type.UuidBytes, Base64FormattingOptions.None); }
			set
			{
				if (value == null) throw new ArgumentNullException("value");
				m_type = new PwUuid(Convert.FromBase64String(value));
			}
		}

		private List<string> m_params = new List<string>();
		[XmlArrayItem("Parameter")]
		public List<string> Parameters
		{
			get { return m_params; }
			set
			{
				if (value == null) throw new ArgumentNullException("value");
				m_params = value;
			}
		}

		private long m_lRunAtTicks = -1;
		[XmlIgnore]
		internal long RunAtTicks
		{
			get { return m_lRunAtTicks; }
			set { m_lRunAtTicks = value; }
		}

		public EcasEvent()
		{
		}

		public EcasEvent CloneDeep()
		{
			EcasEvent e = new EcasEvent();

			e.m_type = m_type; // PwUuid is immutable

			for (int i = 0; i < m_params.Count; ++i)
				e.m_params.Add(m_params[i]);

			return e;
		}

		internal bool RestartTimer()
		{
			if (!m_type.Equals(EcasEventIDs.TimePeriodic)) { Debug.Assert(false); return false; }

			uint s = EcasUtil.GetParamUInt(m_params, 0);
			if (s == 0) return false;

#if DEBUG
			// StackTrace st = new StackTrace(false);
			// Trace.WriteLine("[" + (Environment.TickCount / 1000).ToString() +
			//	"] Restarting timer... (" + st.GetFrame(3).GetMethod().Name +
			//	" -> " + st.GetFrame(2).GetMethod().Name +
			//	" -> " + st.GetFrame(1).GetMethod().Name + ").");
#endif

			DateTime dtNow = DateTime.UtcNow;
			m_lRunAtTicks = dtNow.AddSeconds((double)s - 0.45).Ticks;
			return true;
		}
	}
}
