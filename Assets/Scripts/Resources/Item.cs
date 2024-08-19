using UnityEngine;

namespace Game.Resources {
    public class Item: ScriptableObject {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}