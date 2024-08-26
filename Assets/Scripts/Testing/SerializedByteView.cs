using Game.Serialization;
using Game.Serialization.DataTags;
using UnityEngine;

namespace Game.Testing {
	public class SerializedByteView: MonoBehaviour {
		private byte[] _data;
		private ITag _root;
		private Vector2 _scroll;

		private void Update() {
			if (Input.GetKeyDown(KeyCode.F4)) {
				if (_data != null) {
					SetData(null);
				} else {
					var save = new SaveFile();
					if (save.Exists()) {
						SetData(save.ReadData());
					}
				}
			}
		}

		public void SetData(byte[] data) {
			_data = data;
			if (data != null) {
				_root = TagDeserializer.Deserialize(_data);
			} else {
				_root = null;
			}
		}
		public void OnGUI() {
			if (_root == null) {
				return;
			}
			var result = $"Size: {_data.Length} bytes ({_data.Length / 1024f}Kb)" + DrawTag(_root);
			GUILayout.BeginScrollView(_scroll);
			GUILayout.Label(result);
			GUILayout.EndScrollView();

			string DrawTag(ITag tag, int ident = 0) {
				var result = "\n" + (new string('|', ident)) + $"<{tag.GetType()}> '{tag.Name}': ";
				if (tag is CompoundedTag compounded) {
					foreach (var item in compounded.List) {
						result += $"{DrawTag(item, ident + 1)}";
					}
				}
				if (tag is EntityTag entity) {
					result += $"{entity.Id} ({entity.Guid}) at {entity.Position}";
					result += DrawTag(entity.AdditionalData, ident + 1);
				}
				if (tag is BoolTag boolTag) {
					result += boolTag.Value;
				}
				if (tag is IntTag intTag) {
					result += intTag.Value;
				}
				if (tag is GuidTag guidTag) {
					result += guidTag.Value;
				}
				if (tag is StringTag stringTag) {
					result += stringTag.Value;
				}
				return result;
			}
		}
	}
}