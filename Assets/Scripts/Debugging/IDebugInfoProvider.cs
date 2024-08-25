using System.Text;

namespace Game.Debugging {
	public interface IDebugInfoProvider {
		public void AddDebugInfo(StringBuilder builder);
	}
}