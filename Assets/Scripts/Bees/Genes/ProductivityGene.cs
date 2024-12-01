using UnityEngine;

namespace Game.Bees.Genes {
	[CreateAssetMenu(menuName = "Bee/Genes/Productivity")]
	public class ProductivityGene: Gene {
		[field: SerializeField] public float OutputModifier { get; private set; } = 1;
	}
}