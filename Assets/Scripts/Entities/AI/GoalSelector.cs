using System.Collections.Generic;

namespace Game.Entities.AI {
	public class GoalSelector {
		private List<Goal> _goals = new List<Goal>();
		private Goal _current;
		
		public Goal GetCurrent() => _current;

		public void Add(Goal goal) {
			_goals.Add(goal);
		}
		public void Remove(Goal goal) {
			_goals.Remove(goal);
		}
		public void OnTick() {
			if (_current != null && _current.CanContinueRun() == false) {
				_current.Stop();
				_current = null;
			}
			foreach (var goal in _goals) {
				if (goal.CanStart() && (_current == null || goal.Priority < _current.Priority)) {
					_current?.Stop();
					_current = goal;
					_current.Start();
					break;
				}
			}
			_current?.OnTick();
		}
	}
}