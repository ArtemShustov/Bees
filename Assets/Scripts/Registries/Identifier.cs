using System;
using UnityEngine;

namespace Game.Registries {
	[Serializable]
	public class Identifier: IEquatable<Identifier>, IEquatable<string> {
		[field: SerializeField] public string Full { get; private set; }

		public Identifier(string full) { 
			Full = full;
		}
		public Identifier(string domain, string id) {
			Full = $"{domain}:{id}";
		}

		public override string ToString() {
			return Full;
		}

		public bool Equals(Identifier other) {
			if (other == null) return false;
			return string.Equals(other.ToString(), this.ToString());
		}
		public bool Equals(string other) {
			if (other == null) return false;
			return string.Equals(other, this.ToString());
		}
		public override bool Equals(object obj) {
			return base.Equals(obj);
		}
		public override int GetHashCode() {
			return Full.GetHashCode();
		}

		public static Identifier FromString(string id) {
			return new Identifier(id);
		} 
	}
}