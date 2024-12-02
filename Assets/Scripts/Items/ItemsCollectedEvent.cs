using System.Collections.Generic;
using Game.Events;

namespace Game.Items {
	public class ItemsCollectedEvent: IGameEvent {
		private ItemStack[] _items;

		public IReadOnlyCollection<ItemStack> Items => _items;
		
		public ItemsCollectedEvent(ItemStack[] items) {
			_items = items;
		}
	}
}