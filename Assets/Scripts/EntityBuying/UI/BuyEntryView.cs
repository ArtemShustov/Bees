using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.EntityBuying.UI {
	public class BuyEntryView: MonoBehaviour {
		[SerializeField] private Image _icon;
		[SerializeField] private TMP_Text _name;
		[SerializeField] private string _costPattern = "<sprite name=money>{0}";
		[SerializeField] private TMP_Text _cost;
		[SerializeField] private Button _button;
		private Wallet _wallet;
		private EntityBuyEntry _item;
		
		public EntityBuyEntry Item => _item;
		public event Action<BuyEntryView> Clicked;
		
		public void SetItem(EntityBuyEntry item, Wallet wallet) {
			_item = item;
			_icon.sprite = _item.Icon;
			_name.text = _item.Name;
			_cost.text = string.Format(_costPattern, _item.Cost);
			SetWallet(wallet);
		}
		public void SetWallet(Wallet wallet) {
			if (_wallet) {
				_wallet.ValueChanged -= OnMoneyChanged;
			}
			_wallet = wallet;
			if (enabled) {
				_wallet.ValueChanged += OnMoneyChanged;
			}
			UpdateAvailability();
		}
		
		private void UpdateAvailability() {
			_button.interactable = _wallet.CanTake(_item.Cost);
		}
		
		private void OnMoneyChanged(Wallet obj) {
			UpdateAvailability();
		}
		private void OnButtonClick() {
			Clicked?.Invoke(this);
		}
		private void OnEnable() {
			_button.onClick.AddListener(OnButtonClick);
			if (_wallet != null) {
				_wallet.ValueChanged += OnMoneyChanged;
			}
		}
		private void OnDisable() {
			_button.onClick.RemoveListener(OnButtonClick);
			if (_wallet != null) {
				_wallet.ValueChanged -= OnMoneyChanged;
			}
		}
	}
}