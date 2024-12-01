using Game.Registries;
using UnityEngine;

namespace Game.Serialization {
	public class SerializableObject: MonoBehaviour, IRegistryItem {
		[field: SerializeField] public Identifier Id { get; private set; }

		public void WriteTo(DataTag root) {
			root.SetString(nameof(Id), Id.Full);
			root.Set("_position", transform.position);
			
			var components = GetComponents<ISerializableComponent>();
			foreach (var component in components) {
				component.WriteDataTo(root);
			}
		}
		public void ReadFrom(DataTag root) {
			transform.position = root.Get("_position", Vector3.zero);
			
			var components = GetComponents<ISerializableComponent>();
			foreach (var component in components) {
				component.ReadDataFrom(root);
			}
		}
	}
}