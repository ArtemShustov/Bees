using System.Collections.Generic;
using UnityEngine;

namespace Game.Resources.Containers {
	public class Container: MonoBehaviour {
		[field: Min(1)]
		[field: SerializeField] public int Capacity { get; private set; } = 1;

		private List<ContainerItem> _items = new List<ContainerItem>();

		public bool Add(Item item, int count) {
			return Add(item, count);
		}
		public bool Add(ContainerItem item) {
			return Add(item.Id, item.Count);
		}
		public bool Add(string id, int count) {
			var slot = GetSlot(id);
			if (slot != null) {
				slot.SetCount(slot.Count + count);
				return true;
			}

			if (_items.Count >= Capacity) {
				return false;
			}
			_items.Add(new ContainerItem(id, count));
			return true;
		}
		public bool Take(string id, int count) {
			var slot = GetSlot(id);
			if (slot == null) {
				return false;
			}
			if (slot.Count < count) {
				return false;
			}
			if (slot.Count == count) {
				_items.Remove(slot);
				return true;
			}
			if (slot.Count > count) {
				slot.SetCount(slot.Count - count);
				return true;
			}
			return false;
		}

		public int GetCount(Item item) {
			return GetCount(item.Id);
		}
		public int GetCount(string id) {
			var slot = GetSlot(id);
			return slot == null ? 0 : slot.Count;
		}

		private ContainerItem GetSlot(string id) {
			return _items.Find((item) => string.Equals(item.Id, id));
		}
	}
}