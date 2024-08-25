using System.IO;

namespace Game.Serialization.DataTags {
	public class IntTag: ITag {
		public const byte TagType = 1;
		public byte Type => TagType;

		public string Name { get; private set; }
		public int Value { get; set; }

		public IntTag(string name) {
			Name = name;
		}
		public IntTag(string name, int value) {
			Name = name;
			Value = value;
		}

		public byte[] Serialize() {
			byte[] array;
			using (var stream = new MemoryStream()) {
				stream.WriteByte(TagType);
				stream.WriteStringWithLength(Name);
				stream.WriteInt32(Value);
				array = stream.ToArray();
			}
			return array;
		}

		public static IntTag Create(byte[] bytes) {
			if (bytes == null || bytes.Length < 9 || bytes[0] != TagType) {
				return null;
			}
			IntTag tag;
			using (var stream = new MemoryStream(bytes)) {
				var type = stream.ReadByte();
				var name = stream.ReadStringWithLength();
				var value = stream.ReadInt32();
				tag = new IntTag(name, value);
			}
			return tag;
		}
	}
}