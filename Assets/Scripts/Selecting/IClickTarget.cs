namespace Game.Selecting {
	public interface IClickTarget {
		int Order { get; }
		bool Click();
	}
}