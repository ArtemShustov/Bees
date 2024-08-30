using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Utils {
#if UNITY_EDITOR
	[InitializeOnLoad]
#endif
	public class OnTouchMoveInteraction: IInputInteraction<float> {
		public void Process(ref InputInteractionContext context) {
			switch (context.phase) {
				case InputActionPhase.Waiting:
					if (context.control.IsPressed()) {
						context.Started();
					}
					break;
				case InputActionPhase.Started:
					if (context.control.IsPressed() == false) {
						context.Canceled();
						return;
					}
					if (context.control.device is Touchscreen) {
						if (Touchscreen.current.delta.magnitude > 0) {
							context.PerformedAndStayPerformed();
						}
					}
					break;
				case InputActionPhase.Performed:
					if (context.control.IsPressed() == false) {
						context.Canceled();
					}
					break;
			}
		}

		public void Reset() {
			//
		}

		static OnTouchMoveInteraction() {
			InputSystem.RegisterInteraction<OnTouchMoveInteraction>();
		}
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialize() { }
	}
}