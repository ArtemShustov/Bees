using System;
using System.Linq;
using UnityEngine;

namespace Game.Placing {
	public class PlacingPreview: MonoBehaviour {
		[SerializeField] private Color _cantPlaceColor = new Color(0.6f, 0.6f, 0.6f, 0.6f);
		[SerializeField] private SpriteRenderer _renderer;
		[SerializeField] private float _radius = 0.5f;
		
		private bool _canPlace;

		public event Action<bool> CanPlaceChanged;
		
		public bool CanPlaceHere() {
			var contacts = Physics2D.OverlapCircleAll(transform.position, _radius);
			var nonPlacing = contacts.Any(contact => contact.TryGetComponent<NonPlacingArea>(out var _));
			return !nonPlacing;
		}
		public void SetPosition(Vector2 position) {
			transform.position = position;
			_renderer.color = CanPlaceHere() ? Color.white : _cantPlaceColor;
			
			var canPlace = CanPlaceHere();
			if (canPlace != _canPlace) {
				_canPlace = canPlace;
				CanPlaceChanged?.Invoke(canPlace);
			}
		}

		private void OnDrawGizmos() {
			Gizmos.color = CanPlaceHere() ? Color.green : Color.red;
			Gizmos.DrawWireSphere(transform.position, _radius);
		}
	}
}