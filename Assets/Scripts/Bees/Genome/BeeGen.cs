using System;
using UnityEngine;

namespace Game.Bees.Genome {
	public abstract class BeeGen: ScriptableObject {
		[field: SerializeField] public string Id { get; private set; }
	}
}