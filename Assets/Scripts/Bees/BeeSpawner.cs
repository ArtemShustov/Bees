using Game.EntitySpawner;
using UnityEngine;

namespace Game.Bees {
	[CreateAssetMenu(menuName = "Spawner/Bee")]
	public class BeeSpawner: Spawner {
		[SerializeField] private Bee _prefab;

		public override GameObject Spawn() {
			return Spawn(null).gameObject;
		}
		public Bee Spawn(BeeBase genes) {
			var instance = Instantiate(_prefab);
			instance.SetData(genes);
			return instance;
		}
	}
}