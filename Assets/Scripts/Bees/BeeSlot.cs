using Game.Serialization;

namespace Game.Bees {
	public class BeeSlot {
		public BeeBase Bee { get; private set; }
		public int Timer { get; private set; }

		public BeeSlot() { }
		public BeeSlot(BeeBase bee, int timer) {
			Bee = bee;
			Timer = timer;
		}

		public void Set(BeeBase bee) {
			Bee = bee;
			Timer = 0;
		}
		public bool IsFree() => Bee == null;
		
		public void OnTick() {
			if (Bee != null) {
				Timer++;
			}
		}

		public DataTag ToTag() {
			var root = new DataTag();
			root.SetBool("IsFree", IsFree());
			Bee?.WriteDataTo(root);
			root.SetLong(nameof(Timer), Timer);
			return root;
		}
		public static BeeSlot FromTag(DataTag tag) {
			if (tag == null) {
				return new BeeSlot();
			}
			
			var timer = (int)tag.GetLong(nameof(Timer), 0);
			var isFree = tag.GetBool("IsFree", true);
			if (!isFree) {
				var bee = new BeeBase();
				bee.ReadDataFrom(tag);
				return new BeeSlot(bee, timer);
			}
			return new BeeSlot();
		}
	}
}