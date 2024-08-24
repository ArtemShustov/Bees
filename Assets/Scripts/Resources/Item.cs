using Game.Registries;
using UnityEngine;

namespace Game.Resources {
	public class Item: ScriptableObject, IRegistryItem {
        [field: SerializeField] public Identifier Id { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}