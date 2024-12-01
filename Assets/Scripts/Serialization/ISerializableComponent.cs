namespace Game.Serialization {
	public interface ISerializableComponent {
		void WriteDataTo(DataTag root);
		void ReadDataFrom(DataTag root);
	}
}