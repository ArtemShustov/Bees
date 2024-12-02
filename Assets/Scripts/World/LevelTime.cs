using UnityEngine;

namespace Game.World {
	public class LevelTime: MonoBehaviour {
		[SerializeField] private LevelTime.Time _current;
		
		public LevelTime.Time Current => _current;
		
		public enum Time {
			Day, Night
		}
	}
}