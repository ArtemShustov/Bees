using System;
using System.IO;
using UnityEngine;

namespace Game.Serialization.DataTags {
	public class EntityTag: ITag {
		public const byte TagType = 5;
		public byte Type => TagType;

		public string Name { get; private set; }
		public string Id { get; private set; }
		public Guid Guid { get; set;  }
		public Vector2 Position { get; set; }
		public CompoundedTag AdditionalData { get; private set; }

		public EntityTag(string name, string id) {
			Name = name;
			Id = id;
			AdditionalData = new CompoundedTag(name);
		}
		public EntityTag(string name, string id, Guid guid, Vector2 position, CompoundedTag additionalData) {
			Name = name;
			Id = id;
			Guid = guid;
			Position = position;
			additionalData ??= new CompoundedTag("additional");
			AdditionalData = additionalData;
		}

		public byte[] Serialize() {
			byte[] array;
			using (var stream = new MemoryStream()) {
				stream.WriteByte(TagType);
				stream.WriteStringWithLength(Name);
				stream.WriteStringWithLength(Id);
				stream.WriteGuid(Guid);
				stream.WriteVector2(Position);

				var addData = AdditionalData.Serialize();
				stream.WriteInt32(addData.Length);
				stream.Write(addData);

				array = stream.ToArray();
			}
			return array;
		}
		public static EntityTag Create(byte[] data) {
			if (data == null || data.Length < 32 || data[0] != TagType) {
				return null;
			} 
			EntityTag tag;
			using (var stream = new MemoryStream(data)) {
				var type = (byte)stream.ReadByte();
				var name = stream.ReadStringWithLength();
				var id = stream.ReadStringWithLength();
				var guid = stream.ReadGuid();
				var position = stream.ReadVector2();

				var addLength = stream.ReadInt32();
				var addArray = new byte[addLength];
				stream.Read(addArray);
				var add = addArray[0] == CompoundedTag.TagType ? TagDeserializer.Deserialize(addArray) as CompoundedTag : null;

				tag = new EntityTag(name, id, guid, position, add);
			}
			return tag;
		}
	}
}