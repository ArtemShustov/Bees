using UnityEngine;

namespace Game.GameStates {
	public abstract class GameState: MonoBehaviour {
		public abstract void OnEnter(GameStateSwitch stateSwitch);
		public abstract void OnExit(GameStateSwitch stateSwitch);
	}
}