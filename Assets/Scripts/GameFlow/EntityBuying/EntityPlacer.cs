using System;
using Game.Entities.Registries;
using Game.UI;
using Game.World;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.GameFlow.EntityBuying {
	public class EntityPlacer: MonoBehaviour {
		[SerializeField] private EntityType _entity;
		[SerializeField] private InputAction _pointerPosition;
		[SerializeField] private InputAction _place;
		[SerializeField] private Level _level;
		[SerializeField] private Camera _camera;

		public event Action EntityPlaced;

		public void SetEntity(EntityType entity) {
			_entity = entity;
		}
		
		private void Spawn(Vector2 position) {
			var entity = _entity.SpawnAt(_level.transform, position);
			_level.AddEntityWithNewGUID(entity);
			EntityPlaced?.Invoke();
		}

		private void OnClick(InputAction.CallbackContext obj) {
			if (_entity && !UiUtils.IsPointerOverUI()) {
				var screenPosition = _pointerPosition.ReadValue<Vector2>();
				var worldPosition = (Vector2)_camera.ScreenToWorldPoint(screenPosition);
				Spawn(worldPosition);
			}
		}
		private void OnEnable() {
			_place.Enable();
			_pointerPosition.Enable();
			_place.performed += OnClick;
		}
		private void OnDisable() {
			_place.Disable();
			_pointerPosition.Disable();
			_place.performed -= OnClick;
		}
	}
}