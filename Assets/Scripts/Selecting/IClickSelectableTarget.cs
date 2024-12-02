namespace Game.Selecting {
	public interface IClickSelectableTarget: IClickTarget {
		void OnUnselected();
	}
}