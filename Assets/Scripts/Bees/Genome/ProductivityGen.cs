using System;
using UnityEngine;

namespace Game.Bees.Genome {
	[CreateAssetMenu(menuName = "Gen/Efficiency")]
	public class ProductivityGen: BeeGen, IComparable<ProductivityGen> {
		[field: SerializeField] public float OutputModifier { get; private set; } = 1f;

		public int CompareTo(ProductivityGen other) {
			if (other == null) return 0;
			if (OutputModifier == other.OutputModifier) return 0;
			return OutputModifier > other.OutputModifier ? 1 : -1;
		}
	}
}