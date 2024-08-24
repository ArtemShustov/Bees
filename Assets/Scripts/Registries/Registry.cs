using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Registries {
	[Serializable]
	public class Registry<T>: IRegistry<T> where T: IRegistryItem {
		[SerializeField] private Dictionary<Identifier, T> _map = new Dictionary<Identifier, T>();

		public IReadOnlyList<T> List => _map.Values.ToList().AsReadOnly();
		public T this[Identifier id] => Get(id);

		public T Get(string id) {
			return Get(new Identifier(id));
		}
		public T Get(Identifier id) {
			return _map.TryGetValue(id, out var item) ? item : default;
		}
		public bool Contains(Identifier id) {
			return _map.ContainsKey(id);
		}
		public Identifier Register(T item) {
			if (Contains(item.Id)) {
				return null;
			}
			_map.Add(item.Id, item);
			OnItemAdd(item);
			return item.Id;
		}
		public Identifier IdentifierOf(string id) {
			return List.FirstOrDefault(item => item.Id.Equals(id))?.Id;
		}

		protected virtual void OnItemAdd(T item) {
			//
		}
	}
}