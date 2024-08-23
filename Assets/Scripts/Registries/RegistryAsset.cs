using System.Collections.Generic;
using UnityEngine;

namespace Game.Registries {
	public abstract class RegistryAsset<T>: ScriptableObject where T: IRegistryItem {
		[SerializeField] private T[] _list;
		
		public IReadOnlyCollection<T> List => _list;
		
		public virtual void RegisterAll(IRegistry<T> registry) { 
			if (_list == null) {
				return;
			}
			foreach (var item in _list) {
				registry.Register(item);
			}
		}
		public void SetList(T[] list) {
			_list = list;
		}
	}
}