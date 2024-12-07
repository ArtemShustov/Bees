using System;
using Game.Entities.Registries;
using Game.Placing;
using UnityEngine;

namespace Game.GameFlow.EntityBuying {
	[Serializable]
	public class EntityBuyEntry {
		[field: SerializeField] public EntityType Entity { get; private set; }
		[field: SerializeField] public Sprite Icon { get; private set; }
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField] public int Cost { get; private set; }
		[field: SerializeField] public PlacingPreview Preview { get; private set; }

		public EntityBuyEntry(EntityType entity, Sprite icon, string name, int cost, PlacingPreview preview) {
			Entity = entity;
			Icon = icon;
			Name = name;
			Cost = cost;
			Preview = preview;
		}
	}
}