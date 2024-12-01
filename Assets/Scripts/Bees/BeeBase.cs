using System;
using Game.Bees.Genes;
using Game.Items;
using Game.Registries;
using Game.Serialization;
using UnityEngine;

namespace Game.Bees {
	[Serializable]
	public class BeeBase: ISerializableComponent {
		[field: SerializeField] public ProductGene Product { get; set; }
		[field: SerializeField] public ProductivityGene Productivity { get; set; }

		[field: SerializeField] public bool HasNektar { get; set; }

		public Item GetOutput() => Product.Product;
		public int GetOutputCount() => Product.BasicCount;

		public bool IsValid() {
			return Product && Productivity;
		}
		
		public void WriteDataTo(DataTag root) {
			root.Set(nameof(HasNektar), HasNektar);
			if (Product) {
				root.Set(nameof(Product), Product.Id.ToString());
			}
			if (Productivity) {
				root.Set(nameof(Productivity), Productivity.Id.ToString());
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
		}
	}
}