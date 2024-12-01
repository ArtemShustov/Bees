using Game.Registries;
using UnityEngine;

namespace Game.Bees.Genes {
	public class GeneRegistry: Registry<Gene> {
		protected override void OnItemAdd(Gene item) {
			Debug.Log($"Gene added: {item.Id}");
		}
	}
}