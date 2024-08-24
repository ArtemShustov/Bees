using Game.Debugging;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Game.Resources.Containers {
	public class Container: MonoBehaviour, IDebugInfoProvider {
		[field: Min(1)]
		[field: SerializeField] public int Capacity { get; private set; } = 1;

		private List<ItemStack> _items = new List<ItemStack>();

		public bool Add(Item item, int count) {
			var slot = GetSlot(item.Id.ToString());
			if (slot != null) {
				slot.SetCount(slot.Count + count);
				return true;
			}

			if (_items.Count >= Capacity) {
				return false;
			}
			_items.Add(new ItemStack(item, count));
			return true;
		}
		public bool Add(ItemStack item) {
			return Add(item.Item, item.Count);
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
			return GetCount(item.Id.ToString());
		}
		public int GetCount(string id) {
			var slot = GetSlot(id);
			return slot == null ? 0 : slot.Count;
		}

		private ItemStack GetSlot(string id) {
			return _items.Find(slot => slot.Item.Id.Equals(id));
		}

		public void AddInfo(StringBuilder builder) {
			builder.AppendLine($"Container: capacity {Capacity}");
			foreach (var slot in _items) {
				builder.AppendLine($" * {slot.Item.Id} : {slot.Count}");
			}
		}
	}
}