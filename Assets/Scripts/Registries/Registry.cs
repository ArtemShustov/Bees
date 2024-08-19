using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Registries {
	[Serializable]
	public class Registry<T>: ScriptableObject where T: IRegistryItem {
		[SerializeField] private List<T> _list = new List<T>();

		public IReadOnlyList<T> List => _list.AsReadOnly();
		public T this[string id] => Get(id);

		public T Get(string id) {
			_list = _list.Where((item) => item != null).ToList();
			return _list.Find((item) => string.Equals(item.Id, id));
		}
		public bool Register(T item) {
			if (Get(item.Id) != null) {
				return false;
			}
			_list.Add(item);
			return true;
		}
	}
}