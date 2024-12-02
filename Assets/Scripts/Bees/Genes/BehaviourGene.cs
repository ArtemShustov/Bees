using Game.World;
using UnityEngine;

namespace Game.Bees.Genes {
	[CreateAssetMenu(menuName = "Bee/Genes/Behaviour")]
	public class BehaviourGene: Gene {
		[field: SerializeField] public bool WorkAtDay { get; set; }
		[field: SerializeField] public bool WorkAtNight { get; set; }
	}
}