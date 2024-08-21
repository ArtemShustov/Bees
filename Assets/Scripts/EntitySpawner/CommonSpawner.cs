using UnityEngine;

namespace Game.EntitySpawner {
	[CreateAssetMenu(menuName = "Spawner/Common")]
	public class CommonSpawner: Spawner {
		[SerializeField] private GameObject _prefab;

		public override GameObject Spawn() {
			return Instantiate(_prefab);
		}
	}
}