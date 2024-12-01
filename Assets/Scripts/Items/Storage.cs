using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Game.Registries;
using Game.Serialization;

namespace Game.Items {
	public class Storage {
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
		public bool TryGet(Identifier id, out ItemStack stack) {
			stack = _items.FirstOrDefault(stack => stack.Item.Id.Equals(id));
			return stack != null;
		}
		public void Add(Item item, int count) {
			if (TryGet(item.Id, out ItemStack stack)) {
				stack.Add(count);
			} else {
				_items.Add(new ItemStack(item, count));
			}
		}
		
		public DataTag ToTag() {
			var root = new DataTag();
			var list = _items
				.Where(stack => stack.Count > 0)
				.Select(stack => {
					var tag = new DataTag();
					tag.SetString("Id", stack.Item.Id.ToString());
					tag.SetLong("Count", stack.Count);
					return tag;
				})
				.ToArray();
			root.Set("Items", list);
			return root;
		}
		public void FromTag(DataTag root) {
			if (root == null) {
				return;
			}
			
			var list = root.Get<DataTag[]>("Items", null);
			if (list == null || list.Length == 0) {
				return;
			}
			_items = list
				.Select(tag => ItemStack.FromTag(tag))
				.Where(stack => stack != null)
				.ToList();
		}
	}
}