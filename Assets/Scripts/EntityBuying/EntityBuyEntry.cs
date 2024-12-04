using System;
using Game.Entities.Registries;
using UnityEngine;

namespace Game.EntityBuying {
	[Serializable]
	public class EntityBuyEntry {
		[field: SerializeField] public EntityType Entity { get; private set; }
		[field: SerializeField] public Sprite Icon { get; private set; }
		[field: SerializeField] public string Name { get; private set; }
		[field: SerializeField] public int Cost { get; private set; }

		public EntityBuyEntry(EntityType entity, Sprite icon, string name, int cost) {
			Entity = entity;
			Icon = icon;
			Name = name;
			Cost = cost;
		}
	}
}