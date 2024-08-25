using Game.Bees;
using Game.World;
using UnityEngine;

namespace Game.Testing {
	public class SpawnBeehive: MonoBehaviour {
		[SerializeField] private Flower _flower;
		[SerializeField] private Level _level;

		private void Start() {
			var hive = _level?.Spawn<Beehive>(transform.position);
			if (hive != null) {
				Debug.Log($"Spawned Beehive ({hive.GetGUID()})");
			}
			Destroy(gameObject);
		}
	}
}