using System;
using System.IO;
using System.Linq;
using Game.Debugging;
using Game.Serialization;
using UnityEngine;

namespace Game.Items {
	public class LimitedStorage: Storage, IDebugInfoProvider {
		[SerializeField] private int _max;
		private int _current;

		public int Max => _max;
		public int Count => _current;
		public float Fill => (float)_current / _max;
		public bool IsFull => _current >= _max;

		public event Action FillChanged;
		
		public override bool CanAdd(int count) => _current + count <= _max;
		protected override void Add(Item item, int count) {
			base.Add(item, count);
			_current += count;
			FillChanged?.Invoke();
		}
		protected override void Take(ItemStack stack, int count) {
			base.Take(stack, count);
			_current -= count;
			FillChanged?.Invoke();
		}
		public override ItemStack[] TakeAll() {
			_current = 0;
			FillChanged?.Invoke();
			return base.TakeAll();
		} 

		public override void ReadDataFrom(DataTag root) {
			base.ReadDataFrom(root);
			_current = Items.Sum(item => item.Count);
			FillChanged?.Invoke();
		}
		public virtual void GetDebugInfo(TextWriter writer) { ;
			writer.WriteLine($"LimitedStorage: {_current}/{_max}");
		}
	}
}