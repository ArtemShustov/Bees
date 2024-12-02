using System;
using Game.Bees.Genes;
using Game.Items;
using Game.Registries;
using Game.Serialization;
using Game.World;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Bees {
	[Serializable]
	public class BeeBase: ISerializableComponent {
		[field: SerializeField] public ProductGene Product { get; set; }
		[field: SerializeField] public ProductivityGene Productivity { get; set; }
		[field: SerializeField] public BehaviourGene Behaviour { get; set; }

		[field: SerializeField] public bool HasNektar { get; set; }

		public Item GetOutput() => Product.Product;
		public int GetOutputCount() {
			var output = Product.BasicCount * Productivity.OutputModifier;
			if (output < 1) {
				output = Random.Range(0f, 1f) <= output ? 1 : 0;
			}
			return Mathf.RoundToInt(output);
		}

		public bool CanWork(LevelTime.Time currentTime) {
			if (!Behaviour) {
				return false;
			}
			return currentTime switch {
				LevelTime.Time.Day => Behaviour.WorkAtDay,
				LevelTime.Time.Night => Behaviour.WorkAtNight,
				_ => true
			};
		}
		public bool IsValid() {
			return Product && Productivity && Behaviour;
		}
		
		public void WriteDataTo(DataTag root) {
			root.Set(nameof(HasNektar), HasNektar);
			if (Product) {
				root.Set(nameof(Product), Product.Id.ToString());
			}
			if (Productivity) {
				root.Set(nameof(Productivity), Productivity.Id.ToString());
			}
			if (Behaviour) {
				root.Set(nameof(Behaviour), Behaviour.Id.ToString());
			}
		}
		public void ReadDataFrom(DataTag root) {
			HasNektar = root.Get(nameof(HasNektar), false);
			if (root.TryGetString(nameof(Product), out string product)) {
				Product = GlobalRegistries.Genes.Get(product) is ProductGene gene ? gene : null;
			}
			if (root.TryGetString(nameof(Productivity), out string productivity)) {
				Productivity = GlobalRegistries.Genes.Get(productivity) is ProductivityGene gene ? gene : null;
			}
			if (root.TryGetString(nameof(Behaviour), out string behaviour)) {
				Behaviour = GlobalRegistries.Genes.Get(behaviour) is BehaviourGene gene ? gene : null;
			}
		}
	}
}