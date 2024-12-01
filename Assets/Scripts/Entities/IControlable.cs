using UnityEngine;

namespace Game.Entities {
	public interface IControlable {
		void Move(Vector2 input);
	}
}