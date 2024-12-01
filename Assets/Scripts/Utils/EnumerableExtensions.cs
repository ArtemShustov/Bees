using System.Collections.Generic;
using System.Linq;

namespace Game.Utils {
	public static class EnumerableExtensions {
		public static T GetRandom<T>(this IEnumerable<T> enumerable) {
			return enumerable.ToList()[UnityEngine.Random.Range(0, enumerable.Count())];
		}
	}
}