namespace Game.Registries {
	public interface IRegistry<T> {
		public T Get(string id);
		public T Get(Identifier id);
		public Identifier Register(T item);
	}
}