using UnityEngine;

namespace Game.Bees {
	public class BeeView: MonoBehaviour {
		[SerializeField] private SpriteRenderer _renderer;

		public void SetColor(Color color) {
			_renderer.color = color;
		}
	}
}