using Game.World;

namespace Game.Serialization.DataTags {
	public interface ITagSerializable<T> where T: ITag {
		public void WriteData(T tag);
		public void ReadData(Level level, T tag);
	}
}