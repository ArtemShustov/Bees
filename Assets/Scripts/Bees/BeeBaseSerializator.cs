using Game.Bees.Genome;
using Game.Serialization.DataTags;

namespace Game.Bees {
	public static class BeeBaseSerializator {
		private const string ProductTag = "Product";
		private const string EfficiencyTag = "Efficiency";

		public static CompoundedTag ToTag(string tagName, BeeBase data) {
			if (data == null) {
				return null;
			}
			var root = new CompoundedTag(tagName);
			if (data.ProductGen != null) {
				root.Add(new StringTag(ProductTag, data.ProductGen.Id.ToString()));
			}
			if (data.EfficiencyGen != null) {
				root.Add(new StringTag(EfficiencyTag, data.EfficiencyGen.Id.ToString()));
			}
			return root;
		}
		public static BeeBase FromTag(GenRegistry registry, CompoundedTag data) {
			var product = GetGen<ProductGen>(registry, data?.Get<StringTag>(ProductTag)?.Value);
			var efficiency = GetGen<ProductivityGen>(registry, data?.Get<StringTag>(EfficiencyTag)?.Value);

			return new BeeBase(product, efficiency);
		}
		private static T GetGen<T>(GenRegistry registry, string id) where T: BeeGen {
			if (string.IsNullOrEmpty(id)) {
				return null;
			}
			if (registry.Get(id) is T gen) {
				return gen;
			}
			return null;
		}
	}
}