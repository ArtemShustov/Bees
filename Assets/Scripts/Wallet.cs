using System;
using UnityEngine;

namespace Game {
	public class Wallet: MonoBehaviour {
		private int _value;
		
		public int Value => _value;

		public event Action<Wallet> ValueChanged;

		public void Add(int amount) {
			_value += amount;
			ValueChanged?.Invoke(this);
		}
		public bool CanTake(int amount) => _value >= amount;
		public bool TryTake(int amount) {
			if (!CanTake(amount)) {
				return false;
			}
			Take(amount);
			return true;
		}
		protected void Take(int amount) {
			_value -= amount;
			ValueChanged?.Invoke(this);
		}
	}
}