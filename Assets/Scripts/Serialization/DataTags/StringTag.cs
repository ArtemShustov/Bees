using System.IO;

namespace Game.Serialization.DataTags {
	public class StringTag: ITag {
		public const byte TagType = 3;
		public byte Type => TagType;

		public string Name { get; private set; }
		public string Value { get; set; }

		public StringTag(string name, string value) {
			Name = name;
			Value = value;
		}

		public byte[] Serialize() {
			byte[] array;
			using (var stream = new MemoryStream()) {
				stream.WriteByte(TagType);
				stream.WriteStringWithLength(Name);
				stream.WriteStringWithLength(Value);
				array = stream.ToArray();
			}
			return array;
		}

		public static StringTag Create(byte[] bytes) {
			if (bytes == null || bytes.Length < 9 || bytes[0] != TagType) {
				return null;
			}
			StringTag tag;
			using (var stream = new MemoryStream(bytes)) {
				var type = stream.ReadByte();
				var name = stream.ReadStringWithLength();
				var value = stream.ReadStringWithLength();
				tag = new StringTag(name, value);
			}
			return tag;
		}
	}
}