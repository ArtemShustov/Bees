using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Registers {
	[Serializable]
	public class Register<T>: ScriptableObject where T: IRegisterItem {
		[SerializeField] private List<T> _list = new List<T>();

		public IReadOnlyList<T> List => _list.AsReadOnly();
		public T this[string id] => Get(id);

		public T Get(string id) {
			return _list.Find((item) => string.Equals(item.Id, id));
		}
		public bool Regist(T item) {
			if (Get(item.Id) != null) {
				return false;
			}
			_list.Add(item);
			return true;
		}
	}
}