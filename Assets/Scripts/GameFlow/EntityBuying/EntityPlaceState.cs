using Game.GameFlow.EntityBuying.UI;
using Game.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.GameFlow.EntityBuying {
	public class EntityPlaceState: GameState {
		[SerializeField] private Wallet _wallet;
		[SerializeField] private EntityPlacerWithPreview _placer;
		[SerializeField] private InputAction _placeButton;
		[Header("UI")]
		[SerializeField] private PlaceEntityPanel _panel;
		[SerializeField] private PanelSwitcher _switcher;
		[Header("States")]
		[SerializeField] private GameState _defaultState;
		[SerializeField] private GameStateMachine _machine;

		private EntityBuyEntry _entry;

		public void SetEntry(EntityBuyEntry entry) {
			_entry = entry;
		}

		private void OnPlaceButtonClicked(InputAction.CallbackContext context) {
			if (UiUtils.IsPointerOverUI()) {
				return;
			}
			OnPlaceUiClicked();
		}
		private void OnPlaceUiClicked() {
			if (_entry == null) {
				_machine.ChangeTo(_defaultState);
				return;
			}
			if (_wallet.CanTake(_entry.Cost) && _placer.TryPlaceHere()) {
				_wallet.TryTake(_entry.Cost);
			}
			_placer.StopPlacing();
			_machine.ChangeTo(_defaultState);
		}
		private void OnCancelClicked() {
			_placer.StopPlacing();
			_machine.ChangeTo(_defaultState);
		}

		private void OnCanPlaceChanged(bool state) {
			_panel.SetCanPlace(state);
		}

		public override void OnEnter() {
			_switcher.Switch(_panel);
			
			_panel.PlaceClicked += OnPlaceUiClicked;
			_panel.CancelClicked += OnCancelClicked;
			
			_placer.CanPlaceChanged += OnCanPlaceChanged;
			_placeButton.Enable();
			_placeButton.performed += OnPlaceButtonClicked;
			
			if (_entry != null) {
				_placer.StartPlacing(_entry.Entity, _entry.Preview);
			}
		}
		public override void OnExit() {
			_panel.PlaceClicked -= OnPlaceUiClicked;
			_panel.CancelClicked -= OnCancelClicked;
			
			_placer.CanPlaceChanged -= OnCanPlaceChanged;
			
			_placeButton.Disable();
			_placeButton.performed -= OnPlaceButtonClicked;
		}
	}
}