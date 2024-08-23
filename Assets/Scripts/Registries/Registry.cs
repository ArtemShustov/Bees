using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Registries {
	[Serializable]
	public class Registry<T>: IRegistry<T> where T: IRegistryItem {
		[SerializeField] private List<T> _list = new List<T>();

		public IReadOnlyList<T> List => _list.AsReadOnly();
		public T this[Identifier id] => Get(id);

		public T Get(string id) {
			_list = _list.Where((item) => item != null).ToList();
			return _list.Find((item) => string.Equals(item.Id, id));
		}
		public T Get(Identifier id) {
			return Get(id.ToString());
		}
		public Identifier Register(T item) {
			if (Get(item.Id.ToString()) != null) {
				return null;
			}
			_list.Add(item);
			OnItemAdd(item);
			return item.Id;
		}

		protected virtual void OnItemAdd(T item) {
			//
		}
	}
}