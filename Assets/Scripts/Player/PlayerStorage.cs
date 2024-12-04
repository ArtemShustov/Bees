using Game.Events;
using Game.Items;
using UnityEngine;

namespace Game.Player {
	public class PlayerStorage: Storage {
		private void OnItemsCollected(ItemsCollectedEvent eventData) {
			foreach (var stack in eventData.Items) {
				base.TryAdd(stack);
				Debug.Log($"PlayerStorage collected: {stack.Count} of {stack.Item.Id}");
			}
		}
		
		protected virtual void OnEnable() {
			EventBus<ItemsCollectedEvent>.Event += OnItemsCollected;
		}
		protected virtual void OnDisable() {
			EventBus<ItemsCollectedEvent>.Event -= OnItemsCollected;
		}
	}
}