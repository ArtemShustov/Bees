using System;
using UnityEngine;

namespace Game.Registries {
	[Serializable]
	public class Identifier {
		[field: SerializeField] public string Full { get; private set; }


		public Identifier(string domain, string id) {
			Full = $"{domain}:{id}";
		}

		public override string ToString() {
			return Full;
		}
		public override bool Equals(object obj) {
			if (obj is Identifier key) {
				return string.Equals(key.ToString(), this.ToString());
			}
			return base.Equals(obj);
		}
		public override int GetHashCode() {
			return HashCode.Combine(Full);
		}
	}
}