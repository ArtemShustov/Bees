using System.Collections;
using UnityEngine;

namespace Game.GameStates {
	public class GameStateSwitch: MonoBehaviour {
		[field: SerializeField] public InGameState InGame { get; private set; }
		[field: SerializeField] public MenuState Menu { get; private set; }
		
		private GameState _current;

		public GameState Current => _current;

		private void Start() {
			Change(InGame);
		}
		public void Change(GameState newState) {
			_current?.OnExit(this);
			_current = newState;
			_current.OnEnter(this);
			Debug.Log($"Game state changed to {_current.GetType()}");
		}
	}
}