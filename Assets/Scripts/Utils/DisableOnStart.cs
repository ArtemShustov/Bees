using UnityEngine;

namespace Game.Utils {
	public class DisableOnStart: MonoBehaviour {
		private void Awake() {
			gameObject.SetActive(false);
			Destroy(this);
		}
	}
}