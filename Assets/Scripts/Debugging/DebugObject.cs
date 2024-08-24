using System.Text;
using UnityEngine;

namespace Game.Debugging {
	public class DebugObject: MonoBehaviour {
		public string GetInfo() {
			var builder = new StringBuilder();
			var providers = GetComponents<IDebugInfoProvider>();
			foreach (var provider in providers) {
				provider.AddInfo(builder);
			}
			return builder.ToString();
		}
	}
}