using Game.Registries;
using UnityEngine;

namespace Game.Bees.Genome {
	public abstract class BeeGen: ScriptableObject, IRegistryItem {
		[field: SerializeField] public string Id { get; private set; }
	}
}