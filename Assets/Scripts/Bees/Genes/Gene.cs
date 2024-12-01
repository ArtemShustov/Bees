using Game.Registries;
using UnityEngine;

namespace Game.Bees.Genes {
	public class Gene: ScriptableObject, IGene {
		[field: SerializeField] public Identifier Id { get; private set; }
	}
}