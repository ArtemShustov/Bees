using Game.Entities.Registries;
using Game.UI;
using UnityEngine;

namespace Game.EntityBuying.UI {
	public class PlaceEntityPanel: UIPanel {
		[SerializeField] private EntityPlacer _placer;
		[SerializeField] private PanelSwitcher _switcher;
		private EntityType _entity;
		
		public void SetEntity(EntityType entity) {
			_entity = entity;
		}
		
		public void SwitchToThis() {
			_switcher.Switch(this);
		}

		private void OnEntityPlaced() {
			_switcher.SwitchDefault();
		}
		public override void Show() {
			base.Show();
			_placer.SetEntity(_entity);
			_placer.EntityPlaced += OnEntityPlaced;
		}
		public override void Hide() {
			base.Hide();
			_placer.SetEntity(null);
			_placer.EntityPlaced -= OnEntityPlaced;
		}
	}
}