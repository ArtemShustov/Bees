using UnityEngine;

namespace Game.Resources.Containers {
	public class ContainerItem {
		public string Id { get; private set; }
		public int Count { get; private set; }

		public ContainerItem(string id, int count) {
			Id = id;
			SetCount(count);
		}

		public void SetCount(int count) {
			if (count >= 0) {
				Count = count;
			}
		}
	}
}