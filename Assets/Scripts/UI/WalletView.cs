using TMPro;
using UnityEngine;

namespace Game.UI {
	public class WalletView: MonoBehaviour {
		[SerializeField] private string _pattern = "<sprite name=money>{0}";
		[SerializeField] private TMP_Text _label;
		[SerializeField] private Wallet _wallet;

		private void OnValueChanged(Wallet wallet) {
			_label.text = string.Format(_pattern, wallet.Value);
		}
		private void OnEnable() {
			_wallet.ValueChanged += OnValueChanged;
			OnValueChanged(_wallet);
		}
		private void OnDisable() {
			_wallet.ValueChanged -= OnValueChanged;
		}
	}
}