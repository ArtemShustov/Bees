using System.Collections.Generic;
using UnityEngine;

namespace Game.Resources {
	public class ItemRegister: ScriptableObject {
		[SerializeField] private List<Item> _items = new List<Item>();

		public Item GetItem(string id) {
			return _items.Find(item => item.Id == id);
		}
		public bool Register(Item item) {
			var result = GetItem(item.Id);
			if (result != null) {
				return false;
			}
			_items.Add(item);
			return true;
		}

		#region Singleton
		private static ItemRegister _instance;
		public static string Path { get; private set; } = "Registers/Items";
		public static ItemRegister GetInstance() {
			if (_instance == null ) {
				_instance = UnityEngine.Resources.Load<ItemRegister>(Path);
				if (_instance == null) {
					_instance = ScriptableObject.CreateInstance<ItemRegister>();
				}
			}
			return _instance;
		}
		#endregion
	}
}