using System.Collections.Generic;

namespace Game.Entities.AI {
	public class GoalSelector {
		private List<Goal> _goals = new List<Goal>();
		private Goal _current;
		private int _currentPriority;
		
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
			CheckGoals();

			_current?.OnTick();

			void CheckGoals() {
				var priority = 0;
				foreach (var goal in _goals) {
					if (goal.CanStart() && (_current == null || priority < _currentPriority)) {
						_current?.Stop();
						_current = goal;
						_currentPriority = priority;
						_current.Start();
						break;
					}
					priority++;
				}
			}
		}
	}
}