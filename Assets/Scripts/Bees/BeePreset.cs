using Game.Registries;
using UnityEngine;

namespace Game.Bees {
	[CreateAssetMenu(menuName = "Bee/Preset")]
	public class BeePreset: ScriptableObject {
		[SerializeField] private Identifier _id;
		[SerializeField] private Bee _prefab;
		[SerializeField] private BeeBase _data;
		
		public Identifier Id => _id;
		public Bee Prefab => _prefab;

		public Bee Spawn() {
			var instance = Instantiate(_prefab);
			instance.SetBase(_data);
			return instance;
		}
	}
}