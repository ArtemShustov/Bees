﻿using Game.Registries;
using Game.World;
using UnityEngine;

namespace Game.EntitySpawner {
	public abstract class Spawner: ScriptableObject, IRegistryItem {
		[field: SerializeField] public string Id { get; private set; }

		public abstract GameObject Spawn(Level level);
	}
}