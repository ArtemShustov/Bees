using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

namespace Game {
	public class GlobalInput {
		private readonly GameInput _actions;
		public ControlType ActiveControl { get; private set; }

		private static GlobalInput _instance;
		public static GameInput Actions => _instance._actions;
		public static GlobalInput Current => _instance;

		public event Action<ControlType> ActiveControlChanged;

		public GlobalInput() {
			_actions = new GameInput();
			_instance = this;
			InputUser.listenForUnpairedDeviceActivity += 1;
			InputUser.onUnpairedDeviceUsed += HandleDeviceChange;
			Actions.Common.Enable();

			if (Application.isMobilePlatform) {
				ActiveControl = ControlType.Touch;
			}
			Application.targetFrameRate = 60;
		}
		private void HandleDeviceChange(InputControl control, InputEventPtr eventPtr) {
			var newControl = ControlType.None;
			if (control.device is Keyboard || control.device is Mouse) {
				newControl = ControlType.Keyboard;
			}
			if (control.device is Gamepad) {
				newControl = ControlType.Gamepad;
			}
			if (control.device is Touchscreen) {
				newControl = ControlType.Touch;
			}
			if (newControl != ActiveControl && newControl != ControlType.None) {
				ActiveControl = newControl;
				Debug.Log($"Active control changed to {ActiveControl}.");
				ActiveControlChanged?.Invoke(ActiveControl);

				if (ActiveControl != ControlType.Keyboard) {
					Cursor.lockState = CursorLockMode.Locked;
					Cursor.visible = false;
				} else {
					Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true;
				}
			}
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void LoadRegistries() {
			_instance = new GlobalInput();
		}
	}
	public enum ControlType {
		None = 0,
		Keyboard = 1,
		Gamepad = 2,
		Touch = 3,
	}
}