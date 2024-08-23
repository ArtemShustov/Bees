using System;
using UnityEngine;

namespace Game.Entities {
	public class EntityMovement: MonoBehaviour {
		[field: SerializeField] public float Speed { get; private set; }
		[SerializeField] private Rigidbody2D _rigidbody;

		private Target _target;

		public bool HasTarget => _target != null;

		public event Action<Target> NewTarget;
		public event Action<Target> TargetReached;

		private void Update() {
			if (Input.GetMouseButtonDown(0)) {
				Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				_target = new Target(point);
			}
		}
		private void FixedUpdate() {
			if (_target != null) {
				var toTarget = _target.Point - (Vector2)transform.position;
				if (toTarget.sqrMagnitude == 0) {
					TargetReached?.Invoke(_target);
					_target = null;
					return;
				}

				var movement = Vector2.ClampMagnitude(toTarget, Speed * Time.fixedDeltaTime);
				_rigidbody.MovePosition(transform.position + (Vector3)movement);
			}
		}

		public void SetSpeed(float speed) {
			Speed = Mathf.Abs(speed);
		}
		public void SetTarget(Vector2 point) {
			SetTarget(new Target(point));
		}
		public void SetTarget(Target target) {
			_target = target;
			NewTarget?.Invoke(_target);
		}
		public void MoveIn(Vector2 direction) {
			if (_target == null) {
				_rigidbody.MovePosition(transform.position + (Vector3)direction);
			}
		}

		public class Target {
			public Vector2 Point { get; private set; }

			public Target(Vector2 point) {
				Point = point;
			}
		}
	}
}