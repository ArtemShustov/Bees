using System.IO;

namespace Game.Serialization.DataTags {
	public class BoolTag: ITag {
		public const byte TagType = 2;
		public byte Type => TagType;

		public string Name { get; private set; }
		public bool Value { get; set; }

		public BoolTag(string name, bool value) {
			Name = name;
			Value = value;
		}

		public byte[] Serialize() {
			byte[] array;
			using (var stream = new MemoryStream()) {
				stream.WriteByte(TagType);
				stream.WriteStringWithLength(Name);
				stream.WriteBool(Value);
				array = stream.ToArray();
			}
			return array;
		}

		public static BoolTag Create(byte[] bytes) {
			if (bytes == null || bytes.Length < 6 || bytes[0] != TagType) {
				return null;
			}
			BoolTag tag;
			using (var stream = new MemoryStream(bytes)) {
				var type = stream.ReadByte();
				var name = stream.ReadStringWithLength();
				var value = stream.ReadBool();
				tag = new BoolTag(name, value);
			}
			return tag;
		}
	}
}