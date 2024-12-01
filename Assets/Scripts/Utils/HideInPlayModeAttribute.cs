using System;
using UnityEngine;

namespace Game.Utils {
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class HideInPlayModeAttribute: PropertyAttribute {
		// 
	}
}