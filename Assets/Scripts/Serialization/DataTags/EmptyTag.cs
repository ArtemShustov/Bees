using System.IO;

namespace Game.Serialization.DataTags {
	public class EmptyTag: ITag {
		public const byte TagType = 0;
		public byte Type => TagType;

		public string Name { get; private set; }

		public EmptyTag(string name) {
			Name = name;
		}

		public byte[] Serialize() {
			byte[] array;
			using (var stream = new MemoryStream()) {
				stream.WriteByte(TagType);
				stream.WriteStringWithLength(Name);
				array = stream.ToArray();
			}
			return array;
		}

		public static EmptyTag Create(byte[] bytes) {
			if (bytes == null || bytes.Length < 5 || bytes[0] != TagType) {
				return null;
			}
			EmptyTag tag;
			using (var stream = new MemoryStream(bytes)) {
				var type = stream.ReadByte();
				var name = stream.ReadStringWithLength();
				tag = new EmptyTag(name);
			}
			return tag;
		}
	}
}