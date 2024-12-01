using Game.Serialization;
using UnityEngine;

namespace Game.Entities {
	public class Health: MonoBehaviour, IDamagable, ISerializableComponent {
		private int _health;
		
		public void TakeDamage(int damage) {
			_health -= damage;
		}
		
		public void WriteDataTo(DataTag root) {
			root.SetLong(nameof(_health), _health);
		}
		public void ReadDataFrom(DataTag root) {
			_health = root.Get<int>(nameof(_health), _health);
		}
	}
}