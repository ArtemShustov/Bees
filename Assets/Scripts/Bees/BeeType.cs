using Game.Entities.Registry;
using Game.World;
using UnityEngine;

namespace Game.Bees {
	[CreateAssetMenu(menuName = "Spawner/Bee")]
	public class BeeType: EntityType {
		[SerializeField] private Bee _prefab;

		public override GameObject Spawn(Level level) {
			var instance = Instantiate (_prefab);
			instance.SetWorld(level);
			return instance.gameObject;
		}
		public Bee Spawn(Level level, BeeBase data) {
			var instance = Instantiate(_prefab);
			instance.SetWorld(level);
			instance.SetData(data);
			return instance;
		}
	}
}