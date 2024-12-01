using UnityEngine;

namespace Game.Entities {
	public class SimpleMovement: Movement {
		[SerializeField] private float _speed = 2;
		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private Transform _model;
		
		private Direction _direction;
		private Vector2 _input;
		
		public override void Move(Vector2 input) {
			_input = input;
		}

		private void FixedUpdate() {
			_rigidbody.MovePosition(_rigidbody.position + _input * (_speed * Time.fixedDeltaTime));
			var direction = _input.x switch {
				0 => _direction,
				> 0 => Direction.Right,
				< 0 => Direction.Left,
				_ => _direction
			};
			if (direction != _direction) {
				_direction = direction;
				var xScale = _direction == Direction.Left ? -1 : 1;
				_model.localScale = new Vector3(xScale, 1, 1);
			}
		}

		public enum Direction {
			Right, Left
		}
	}
}