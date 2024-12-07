using UnityEngine;

namespace Game.GameFlow {
	public abstract class GameState: MonoBehaviour, IGameState {
		public abstract void OnEnter();
		public abstract void OnExit();
	}
}