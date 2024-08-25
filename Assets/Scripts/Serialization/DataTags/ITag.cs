namespace Game.Serialization.DataTags {
	public interface ITag {
		public byte Type { get; }
		public byte[] Serialize();
		public string Name { get; }
	}
}