using Game.Serialization.DataTags;
using System;

namespace Game.Serialization {
	public static class TagDeserializer {
		public static ITag Deserialize(byte[] bytes) {
			return bytes[0] switch {
				EmptyTag.TagType => EmptyTag.Create(bytes),
				IntTag.TagType => IntTag.Create(bytes),
				StringTag.TagType => StringTag.Create(bytes),
				BoolTag.TagType => BoolTag.Create(bytes),
				CompoundedTag.TagType => CompoundedTag.Create(bytes),
				EntityTag.TagType => EntityTag.Create(bytes),
				GuidTag.TagType => GuidTag.Create(bytes),
				_ => null,
			};
		}
		public static Type GetTagType(byte[] bytes) {
			return bytes[0] switch {
				EmptyTag.TagType => typeof(EmptyTag),
				IntTag.TagType => typeof(IntTag),
				StringTag.TagType => typeof(StringTag),
				BoolTag.TagType => typeof(BoolTag),
				CompoundedTag.TagType => typeof(CompoundedTag),
				EntityTag.TagType => typeof(EntityTag),
				GuidTag.TagType => typeof(GuidTag),
				_ => null,
			};
		}
	}
}