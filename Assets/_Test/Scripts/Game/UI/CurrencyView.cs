using Test.AsyncFactory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Game.UI
{
    public sealed class CurrencyView : MonoBehaviour
    {
        [SerializeField]
        private CurrencyType _type;
        [SerializeField]
        private TMP_Text _valueText;
        [SerializeField]
        private Image _icon;
        public double Value { get; private set; }

        private Currency Currency => _type.GetCurrency();

        private CurrenciesConfig.CurrencyData Data => _type.GetData();

        private void Awake()
        {
            var currency = Currency;
            Value = currency.Value;
            currency.ValueChanged += OnValueChanged;

            _icon.sprite = Data.Icon;
        }

        private void OnEnable()
        {
            updateView();
        }

        public void updateView()
        {
            updateView(Currency.Value);
        }
        private void updateView(double value)
        {
            _valueText.text = value.ToString();
        }
       
        private void OnValueChanged(CurrencyType currencyType, double value)
        {
            Value = value;
            updateView();
        }

        private void OnDestroy()
        {
            Currency.ValueChanged -= OnValueChanged;
        }

    }
}
