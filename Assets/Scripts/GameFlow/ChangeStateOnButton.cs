using UnityEngine;
using UnityEngine.UI;

namespace Game.GameFlow {
	public class ChangeStateOnButton: MonoBehaviour {
		[SerializeField] private Button _button;
		[SerializeField] private GameState _state;
		[SerializeField] private GameStateMachine _machine;

		private void OnClick() {
			_machine.ChangeTo(_state);
		}

		private void OnEnable() {
			_button.onClick.AddListener(OnClick);
		}
		private void OnDisable() {
			_button.onClick.RemoveListener(OnClick);
		}
	}
}