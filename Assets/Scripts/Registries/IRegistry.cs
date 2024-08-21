namespace Game.Registries {
	public interface IRegistry<T> {
		public T Get(string id);
	}
}