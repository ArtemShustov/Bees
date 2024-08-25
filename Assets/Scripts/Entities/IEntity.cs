using Game.World;
using System;

namespace Game.Entities {
	public interface IEntity {
		public void SetWorld(Level level);
		public Guid GetGUID();
	}
}