using System.IO;
using Game.Entities;
using Game.Serialization;
using UnityEngine;

namespace Game.Bees {
	[SelectionBase]
	public class Bee: Entity {
		[field: SerializeField] public BeeBase Base { get; private set; }
		[field: SerializeField] public BeeView View { get; private set; }
		[field: SerializeField] public BeeAiBrain AiBrain { get; private set; }

		private void Awake() {
			SetBase(Base);
		}
		
		public virtual void SetBase(BeeBase data) {
			Base = data;
			if (Base.Product) {
				View.SetColor(data.Product.Color);
			}
		}
		public void SetNektar(bool nektar) {
			Base.HasNektar = nektar;
			// View.SetNektar(nektar);
		}
		public virtual void SetHome(Beehive beehive) => AiBrain.SetHome(beehive);
		public virtual void SetFlower(Flower flower) => AiBrain.SetFlower(flower);

		public override void WriteDataTo(DataTag root) {
			Base.WriteDataTo(root);
			base.WriteDataTo(root);
		}
		public override void ReadDataFrom(DataTag root) {
			Base.ReadDataFrom(root);
			SetBase(Base);
			base.ReadDataFrom(root);
		}
		public override void GetDebugInfo(TextWriter writer) {
			base.GetDebugInfo(writer);
			writer.WriteLine($"> HasNektar: {Base.HasNektar}");
			writer.WriteLine($"> Product: {Base.Product?.Id}");
			writer.WriteLine($"> Productivity: {Base.Productivity?.Id}");
		}
	}
}