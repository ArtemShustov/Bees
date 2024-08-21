using UnityEngine;

namespace Game.Resources.Containers {
	public class ItemStack {
		public Item Item { get; private set; }
		public int Count { get; private set; }

		public ItemStack(Item item, int count = 1) {
			Item = item;
			SetCount(count);
		}

		public void SetCount(int count) {
			if (count >= 0) {
				Count = count;
			}
		}
	}
}