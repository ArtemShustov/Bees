using Game.Serialization.DataTags;
using Game.World;
using System;

namespace Game.Bees {
	public partial class Beehive {
		[Serializable]
		private class BeeSlot: ITagSerializable<CompoundedTag> {
			public BeeBase Bee { get; private set; }
			public int Timer { get; private set; } = 0;
			public bool HasNektar { get; private set; } = false;

			public bool IsFree => Bee == null;

			public BeeSlot(Level level) {
				Timer = 0; 
			}
			public BeeSlot(BeeBase bee, bool hasNektar) {
				Bee = bee;
				Timer = 0;
				HasNektar = hasNektar;
			}

			public void SetBee(BeeBase bee, bool hasNektar) {
				Bee = bee;
				Timer = 0;
				HasNektar = hasNektar;
			}

			public void OnTick() {
				if (Bee != null) {
					Timer += 1;
				}
			}

			public void WriteData(CompoundedTag tag) {
				tag.Add(new BoolTag(nameof(HasNektar), HasNektar));
				tag.Add(new IntTag(nameof(Timer), Timer));
				if (Bee != null) {
					tag.Add(BeeBaseSerializator.ToTag(nameof(Bee), Bee));
				}
			}
			public void ReadData(Level level, CompoundedTag tag) {
				HasNektar = tag.Get<BoolTag>(nameof(HasNektar))?.Value ?? false;
				Timer = tag.Get<IntTag>(nameof(Timer))?.Value ?? 0;
				var beeTag = tag.Get<CompoundedTag>(nameof(Bee));
				if (level != null && beeTag != null) {
					Bee = BeeBaseSerializator.FromTag(level.GenRegistry, beeTag);
				}
			}
		}
	}
}