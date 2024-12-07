namespace Game.GameFlow {
	public interface IGameState {
		void OnEnter();
		void OnExit();
	}
}