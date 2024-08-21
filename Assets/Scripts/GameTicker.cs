using Game.Ticking;
using UnityEngine;

namespace Game {
	public static class GameTicker {
		private static TickManager _instance;

		public static TickManager Current => GetCurrent();

		public static TickManager GetCurrent() {
			if (_instance == null) {
				_instance = CreateInstance();
			}
			return _instance;
		}
		private static TickManager CreateInstance() {
			var instance = new GameObject("TickManaher", typeof(TickManager)).GetComponent<TickManager>();
			GameObject.DontDestroyOnLoad(instance.gameObject);
			return instance;
		}
	}
}