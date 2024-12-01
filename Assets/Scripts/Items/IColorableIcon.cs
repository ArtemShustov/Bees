using UnityEngine;

namespace Game.Items {
	public interface IColorableIcon {
		Sprite Icon { get; }
		Color Color { get; }
	}
}