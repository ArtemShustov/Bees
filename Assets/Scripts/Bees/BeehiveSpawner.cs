using Game.EntitySpawner;
using Game.World;
using UnityEngine;

namespace Game.Bees {
	[CreateAssetMenu(menuName = "Spawner/Beehive")]
	public class BeehiveSpawner: Spawner {
		[SerializeField] private Beehive _prefab;

		public override GameObject Spawn(Level level) {
			var instance = Instantiate(_prefab);
			instance.SetWorld(level);
			return instance.gameObject;
		}
	}
}