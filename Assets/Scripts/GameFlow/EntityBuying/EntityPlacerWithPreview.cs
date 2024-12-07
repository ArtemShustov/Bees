using System;
using Game.Entities;
using Game.Entities.Registries;
using Game.Placing;
using Game.UI;
using Game.World;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.GameFlow.EntityBuying {
	public class EntityPlacerWithPreview: MonoBehaviour {
		[SerializeField] private InputAction _pointerPosition;
		[SerializeField] private Camera _camera;
		[SerializeField] private Level _level;
		
		private EntityType _entity;
		private PlacingPreview _preview;
		private bool _canPlace;

		public event Action<Entity> Placed;
		public event Action<bool> CanPlaceChanged;

		private void UpdatePreviewPosition(InputAction.CallbackContext context) {
			if (_preview) {
				var screenPosition = _pointerPosition.ReadValue<Vector2>();
				if (UiUtils.IsOverUI(screenPosition)) {
					return;
				}
				var position = _camera.ScreenToWorldPoint(screenPosition);
				_preview.SetPosition(position);
			}
		}
		
		public void StartPlacing(EntityType entity, PlacingPreview previewPrefab) {
			if (_preview) {
				Destroy(_preview.gameObject);
			}
			_entity = entity;
			_preview = Instantiate(previewPrefab, transform);
			_preview.CanPlaceChanged += PreviewOnCanPlaceChanged;
		}
		public void StopPlacing() {
			if (_preview) {
				_preview.CanPlaceChanged -= PreviewOnCanPlaceChanged;
				Destroy(_preview.gameObject);
			}
		}
		public bool TryPlaceHere() {
			if (_entity == null || _preview == null) {
				return false;
			}
			if (!_preview.CanPlaceHere()) {
				return false;
			}
			var entity = _entity.SpawnAt(_level.transform, _preview.transform.position);
			_level.AddEntityWithNewGUID(entity);
			Placed?.Invoke(entity);
			return true;
		}

		private void PreviewOnCanPlaceChanged(bool state) {
			CanPlaceChanged?.Invoke(state);
		}
		private void OnEnable() {
			_pointerPosition.Enable();
			_pointerPosition.performed += UpdatePreviewPosition;
		}
		private void OnDisable() {
			_pointerPosition.Disable();
			_pointerPosition.performed -= UpdatePreviewPosition;
		}
	}
}
