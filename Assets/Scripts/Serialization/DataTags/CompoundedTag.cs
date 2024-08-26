using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Game.Serialization.DataTags {
	public class CompoundedTag: ITag {
		public const byte TagType = 4;
		public byte Type => TagType;

		public string Name { get; private set; }
		private List<ITag> _list = new List<ITag>();

		public IReadOnlyList<ITag> List => _list;

		public CompoundedTag(string name) {
			Name = name;
		}

		public void Add(ITag tag) {
			_list.Add(tag);
		}
		public void Remove(ITag tag) {
			_list.Remove(tag);
		}
		public bool Contains(string key) { 
			return _list.FirstOrDefault(item => item.Name.Equals(key)) != null;
		}
		public T Get<T>(string key) where T: ITag {
			var tag = _list.FirstOrDefault(t => t.Name.Equals(key));
			return tag != null ? (T)tag : default;
		}

		public byte[] Serialize() {
			byte[] array;
			using (var stream = new MemoryStream()) {
				stream.WriteByte(TagType);
				stream.WriteStringWithLength(Name);
				stream.WriteInt32(_list.Count);
				foreach (var tag in _list) {
					if (tag == null) {
						continue;
					}
					var tagArray = tag.Serialize();
					stream.WriteInt32(tagArray.Length);
					stream.Write(tagArray);
				}
				array = stream.ToArray();
			}
			return array;
		}
		public static CompoundedTag Create(byte[] bytes) {
			if (bytes == null || bytes.Length < 9 || bytes[0] != TagType) {
				return null;
			}
			CompoundedTag root;
			using (var stream = new MemoryStream(bytes)) {
				var type = stream.ReadByte();
				var name = stream.ReadStringWithLength();
				var len = stream.ReadInt32();
				root = new CompoundedTag(name);

				for (int i = 0; i < len; i++) {
					var tagLen = stream.ReadInt32();
					if (tagLen <= 0) {
						continue;
					}
					var tagArray = new byte[tagLen];
					stream.Read(tagArray);
					var tag = TagDeserializer.Deserialize(tagArray);
					if (tag != null) {
						root.Add(tag);
					}
				}
			}
			return root;
		}
	}
}