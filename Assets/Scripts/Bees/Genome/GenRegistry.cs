using Game.Registries;
using System.Linq;

namespace Game.Bees.Genome {
	public class GenRegistry: Registry<BeeGen> {
		public static readonly string Name = "BeeGenes";
		
		public T[] GetGroup<T>() where T: BeeGen {
			var group = from gen in List where gen is T select gen as T;
			return group.ToArray();
		}
	}
}