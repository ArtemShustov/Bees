using UnityEngine;

namespace Game.GameFlow {
	public class GameStateMachine: MonoBehaviour {
		private IGameState _current;

		public IGameState Current => _current;
		
		public void ChangeTo(IGameState state) {
			_current?.OnExit();
			_current = state;
			_current?.OnEnter();
		}
	}
}