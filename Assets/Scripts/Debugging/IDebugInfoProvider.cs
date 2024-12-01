using System.IO;

namespace Game.Debugging {
	public interface IDebugInfoProvider {
		void GetDebugInfo(TextWriter writer);
	}
}