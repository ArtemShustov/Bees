using Game.Bees.Genome;
using System;
using UnityEngine;

namespace Game.Bees {
	[Serializable]
	public class BeeBase {
		[field: SerializeField] public ProductGen ProductGen { get; private set; }
		[field: SerializeField] public ProductivityGen EfficiencyGen { get; private set; }

		public BeeBase(ProductGen productGen, ProductivityGen efficiencyGen) {
			ProductGen = productGen;
			EfficiencyGen = efficiencyGen;
		}

		public string GetOutput() {
			return ProductGen?.Resource;
		}
		public int GetOutputCount() {
			if (ProductGen == null || EfficiencyGen == null) {
				return 0;
			}
			return Mathf.RoundToInt(ProductGen.BaseCount * EfficiencyGen.OutputModifier - 0.01f); // 0.01f due to global warming?
		}
	}
}