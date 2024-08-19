using Game.Bees.Genome;
using UnityEngine;

namespace Game.Bees {
	public class BeeBase {
		[SerializeField] private ProductGen _product;
		[SerializeField] private ProductivityGen _efficiency;
	}
}