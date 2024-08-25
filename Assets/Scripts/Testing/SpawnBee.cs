using Game.Bees;
using Game.World;
using UnityEngine;

namespace Game.Testing {
	public class SpawnBee: MonoBehaviour {
		[SerializeField] private BeeBase _base;
		[SerializeField] private Beehive _home;
		[SerializeField] private Flower _flower;
		[SerializeField] private Level _level;

		private void Start() {
			var bee = _level?.Spawn<Bee>(transform.position);
			if (bee != null) {
				bee.SetData(_base, _home, _flower);
				Debug.Log($"Spawned Bee ({bee.GetGUID()})");
			}
			Destroy(gameObject);
		}
	}
}