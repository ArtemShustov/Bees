﻿using Game.Debugging;
using Game.Entities.AI;
using System.Text;
using UnityEngine;

namespace Game.Entities {
	[RequireComponent(typeof(EntityMovement))]
	public class LivingEntity: Entity, IDebugInfoProvider {
		[field: SerializeField] public EntityMovement Movement { get; private set; }
		[field: SerializeField] public GoalSelector GoalSelector { get; private set; } = new GoalSelector();

		protected virtual void Awake() {
			Movement = GetComponent<EntityMovement>();
		}

		public override void OnTick() {
			GoalSelector.OnTick();
		}

		public void AddInfo(StringBuilder builder) {
			builder.AppendLine($"Living entity: {this.GetType()}");
			builder.AppendLine($" * Goal: {GoalSelector?.GetCurrent()?.GetType()}");
			if (GoalSelector?.GetCurrent() is IDebugInfoProvider goal) {
				goal.AddInfo(builder);
			}
		}
	}
}