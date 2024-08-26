using Game.Debugging;
using Game.Serialization.DataTags;
using Game.World;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Game.Resources.Containers {
	[Serializable]
	public class Container: IDebugInfoProvider, ITagSerializable<CompoundedTag> {
		[field: Min(1)]
		[field: SerializeField] public int Capacity { get; private set; }

		private List<ItemStack> _items = new List<ItemStack>();
		public IReadOnlyList<ItemStack> List => _items;

		public Container(int capacity = 1) {
			Capacity = capacity;
		}

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
		public void Clear() {
			_items = new List<ItemStack>();
		}

		public int GetCount(Item item) {
			return GetCount(item.Id.ToString());
		}
		public int GetCount(string id) {
			var slot = GetSlot(id);
			return slot == null ? 0 : slot.Count;
		}
		public void SetCapacity(int count) {
			Capacity = count;
			if (_items.Count < Capacity) {
				_items = _items.GetRange(0, Capacity);
			}
		}

		private ItemStack GetSlot(string id) {
			return _items.Find(slot => slot.Item.Id.Equals(id));
		}
	
		public void WriteData(CompoundedTag tag) {
			tag.Add(new IntTag(nameof(Capacity), Capacity));
			var itemsTag = new CompoundedTag(nameof(_items));
			var items = List;
			for (int i = 0; i < items.Count; i++) {
				var slot = items[i];
				var slotTag = new CompoundedTag($"Slot{i}");
				slotTag.Add(new IntTag("Count", slot.Count));
				slotTag.Add(new StringTag("Id", slot.Item.Id.ToString()));
				itemsTag.Add(slotTag);
			}
			tag.Add(itemsTag);
		}
		public void ReadData(Level level, CompoundedTag tag) {
			Capacity = tag.Get<IntTag>(nameof(Capacity))?.Value ?? 0;
			var itemsTag = tag.Get<CompoundedTag>(nameof(_items));
			if (itemsTag == null) {
				return;
			}
			foreach (var rawTag in itemsTag.List) {
				if (rawTag is CompoundedTag slot) {
					var count = slot.Get<IntTag>("Count")?.Value ?? 0;
					if (count <= 0) {
						continue;
					}
					var id = slot.Get<StringTag>("Id")?.Value;
					var item = level.ItemRegistry.Get(id);
					if (item != null) {
						var stack = new ItemStack(item, count);
						_items.Add(stack);
					}
				}
			}
		}

		public void AddDebugInfo(StringBuilder builder) {
			builder.AppendLine($"Container: capacity {Capacity}");
			foreach (var slot in _items) {
				builder.AppendLine($" * {slot.Item.Id} : {slot.Count}");
			}
		}
	}
}