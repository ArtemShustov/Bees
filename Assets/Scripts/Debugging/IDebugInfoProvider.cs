﻿using System.Text;

namespace Game.Debugging {
	public interface IDebugInfoProvider {
		public void AddInfo(StringBuilder builder);
	}
}