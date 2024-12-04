using UnityEditor;
using UnityEngine;

namespace Game.Player.Editor {
	[CustomEditor(typeof(PlayerMoneyWallet))]
	public class PlayerMoneyWalletEditor: UnityEditor.Editor {
		private int _cheatMoney;
		
		public override void OnInspectorGUI() {
			DrawDefaultInspector();

			if (!Application.isPlaying) {
				return;
			}
			PlayerMoneyWallet wallet = (PlayerMoneyWallet)target;
			GUILayout.Label($"Value: {wallet.Value}");
			
			_cheatMoney = EditorGUILayout.IntField("Cheat Money", _cheatMoney);
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Add")) {
				wallet.Add(_cheatMoney);
			}
			if (GUILayout.Button("Take")) {
				wallet.TryTake(_cheatMoney);
			}
			GUILayout.EndHorizontal();
			
			if (GUILayout.Button("Do 'motherlode' (+50.000)")) {
				wallet.Add(50_000);
			}
		}
	}
}