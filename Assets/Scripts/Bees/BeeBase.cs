using Game.Bees.Genome;
using System;
using UnityEngine;

namespace Game.Bees {
	[Serializable]
	public class BeeBase {
		[field: SerializeField] public ProductGen ProductGen { get; private set; }
		[field: SerializeField] public ProductivityGen EfficiencyGen { get; private set; }

		[field: SerializeField] public bool HasNektar { get; set; }

		public string GetOutput() {
			return ProductGen.Resource;
		}
		public int GetOutputCount() {
			return Mathf.RoundToInt(ProductGen.BaseCount * EfficiencyGen.OutputModifier - 0.01f); // 0.01f due to global warming?
		}
	}
}