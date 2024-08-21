using Game.Resources;
using UnityEngine;

namespace Game.Bees.Genome {
	[CreateAssetMenu(menuName = "Gen/Product")]
	public class ProductGen: BeeGen {
		[field: ItemSearch]
		[field: SerializeField] public string Resource { get; private set; }
		[field: SerializeField] public int BaseCount { get; private set; }

		// void ApplyEffect(Beehive beehive)
	}
}