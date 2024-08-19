using UnityEngine;

namespace Game.Bees.Genome {
	[CreateAssetMenu(menuName = "Gen/Efficiency")]
	public class ProductivityGen: BeeGen {
		[field: SerializeField] public float Modifier { get; private set; } = 1f;
	}
}