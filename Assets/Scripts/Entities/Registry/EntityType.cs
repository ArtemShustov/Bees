﻿using Game.Registries;
using Game.World;
using UnityEngine;

namespace Game.Entities.Registry {
	[CreateAssetMenu(menuName = "Entities/Type")]
	public class EntityType: ScriptableObject, IRegistryItem {
		[field: SerializeField] public Identifier Id { get; private set; }
		[field: SerializeField] public Entity Prefab { get; private set; }

		public Entity Spawn(Level level) {
			var instance = Instantiate(Prefab);
			instance.SetWorld(level);
			return instance;
		}
	}
}