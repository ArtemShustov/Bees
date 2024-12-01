using Game.Items;
using UnityEngine;

namespace Game.Bees.Genes {
	[CreateAssetMenu(menuName = "Bee/Genes/Product")]
	public class ProductGene: Gene {
		[field: SerializeField] public Color Color { get; private set; } = Color.white;
		[field: SerializeField] public Item Product { get; private set; }
		[field: SerializeField] public int BasicCount { get; private set; } = 1;
	}
}