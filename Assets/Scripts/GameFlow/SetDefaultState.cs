using UnityEngine;

namespace Game.GameFlow {
	public class SetDefaultState: MonoBehaviour {
		[SerializeField] private GameState _state;
		[SerializeField] private GameStateMachine _machine;

		private void Awake() {
			_machine.ChangeTo(_state);
		}
	}
}