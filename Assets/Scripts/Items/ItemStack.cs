using System;
using Game.Registries;
using Game.Serialization;
using UnityEngine;

namespace Game.Items {
	[Serializable]
	public class ItemStack {
		[field: SerializeField] public Item Item { get; private set; }
		[field: SerializeField] public int Count { get; private set; }

		public ItemStack(Identifier id, int count = 0) {
			Item = GlobalRegistries.Items.Get(id);
			Count = count;
		}
		public ItemStack(Item item, int count = 0) {
			Item = item;
			Count = count;
		}
		
		public void SetCount(int count) {
			Count = Mathf.Max(0, count);
		}
		public void Add(int count) {
			Count += count;
		}

		public DataTag ToTag() {
			var root = new DataTag();
			root.SetString("id", Item.Id.ToString());
			root.SetLong("count", Count);
			return root;
		}
		public static ItemStack FromTag(DataTag tag) {
			if (tag.TryGetString("id", out var id) && tag.TryGetLong("count", out var count)) {
				var item = GlobalRegistries.Items.Get(id);
				if (item != null) {
					return new ItemStack(item, (int)count);
				}
			}
			return null;
		}
	}
}