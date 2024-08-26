using System;
using System.IO;

namespace Game.Serialization.DataTags {
	public class GuidTag: ITag {
		public const byte TagType = 6;
		public byte Type => TagType;

		public string Name { get; private set; }
		public Guid Value { get; set; }

		public GuidTag(string name, Guid value) {
			Name = name;
			Value = value;
		}

		public byte[] Serialize() {
			byte[] array;
			using (var stream = new MemoryStream()) {
				stream.WriteByte(TagType);
				stream.WriteStringWithLength(Name);
				stream.WriteGuid(Value);
				array = stream.ToArray();
			}
			return array;
		}

		public static GuidTag Create(byte[] bytes) {
			if (bytes == null || bytes.Length < 6 || bytes[0] != TagType) {
				return null;
			}
			GuidTag tag;
			using (var stream = new MemoryStream(bytes)) {
				var type = stream.ReadByte();
				var name = stream.ReadStringWithLength();
				var value = stream.ReadGuid();
				tag = new GuidTag(name, value);
			}
			return tag;
		}
	}
}