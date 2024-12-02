using System;
using System.Collections.Generic;
using System.Linq;
using Game.Registries;
using Game.Serialization;
using UnityEngine;

namespace Game.Items {
	public class Storage: MonoBehaviour, ISerializableComponent {
		private List<ItemStack> _items = new List<ItemStack>();
		
		public IReadOnlyList<ItemStack> Items => _items;

		public bool Contains(Identifier id) {
			return _items.FirstOrDefault(stack => stack.Item.Id.Equals(id)) != null;
		}
		public bool Contains(Identifier id, int count) {
			if (TryGet(id, out ItemStack stack)) {
				return stack.Count <= count;
			}
			return false;
		}
		protected bool TryGet(Identifier id, out ItemStack stack) {
			stack = _items.FirstOrDefault(stack => stack.Item.Id.Equals(id));
			return stack != null;
		}

		public virtual ItemStack[] TakeAll() {
			var stacks = _items.ToArray();
			_items.Clear();
			return stacks;
		}
		public virtual bool TryTake(Identifier id, int count) {
			if (TryGet(id, out ItemStack stack) && stack.Count >= count) {
				Take(stack, count);
				return true;
			}
			return false;
		}
		protected virtual void Take(ItemStack stack, int count) {
			stack.Take(count);
			if (stack.Count <= 0) {
				_items.Remove(stack);
			}
		}

		public virtual bool CanAdd(int count) => true;
		public virtual bool TryAdd(Item item, int count) {
			if (CanAdd(count)) {
				Add(item, count);
				return true;
			}
			return false;
		}
		public virtual bool TryAdd(ItemStack stack) {
			return TryAdd(stack.Item, stack.Count);
		}
		protected virtual void Add(Item item, int count) {
			if (TryGet(item.Id, out ItemStack stack)) {
				stack.Add(count);
			} else {
				_items.Add(new ItemStack(item, count));
			}
		}

		public virtual void WriteDataTo(DataTag root) {
			var list = _items
				.Where(stack => stack.Count > 0)
				.Select(stack => {
					var stackTag = new DataTag();
					stackTag.SetString("Id", stack.Item.Id.ToString());
					stackTag.SetLong("Count", stack.Count);
					return stackTag;
				})
				.ToArray();
			root.Set("Items", list);
		}
		public virtual void ReadDataFrom(DataTag root) {
			if (root == null) {
				return;
			}
			
			var list = root.Get<DataTag[]>("Items", null);
			if (list == null || list.Length == 0) {
				return;
			}
			_items = list
				.Select(stackTag => ItemStack.FromTag(stackTag))
				.Where(stack => stack != null)
				.ToList();
		}
		[Obsolete]
		public DataTag ToTag() {
			var root = new DataTag();
			WriteDataTo(root);
			return root;
		}
		[Obsolete]
		public void FromTag(DataTag root) {
			ReadDataFrom(root);
		}
	}
}
