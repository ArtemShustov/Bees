using UnityEngine;

namespace Game.Entities {
	public abstract class Movement: MonoBehaviour, IControlable {
		public abstract void Move(Vector2 input);
	}
}