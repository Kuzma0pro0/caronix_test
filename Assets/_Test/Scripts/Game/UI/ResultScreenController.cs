using System;
using Test.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Game.UI
{
    public sealed class ResultScreenController : BaseScreen
    {
        public Action OnContinue;

        [SerializeField]
        private TMP_Text _info;
        [SerializeField]
        private TMP_Text _coins;
        [SerializeField]
        private Image _coinsIcon;

        [SerializeField]
        private Button _continueBttn;

        private Enemy _enemy;
        private int _addCoins;

        private void Start()
        {
            _continueBttn.onClick.AddListener(Continue);     
        }

        public override void Init(params object[] param)
        {
            base.Init(param);

            if (param.Length > 0 && param[0] is Enemy)
            {
                _enemy = (Enemy)param[0];
            }

            if (param.Length > 1 && param[1] is int)
            {
                _addCoins = (int)param[1];
            }

            UpdateView();
        }

        private void UpdateView()
        {
            _info.text = string.Format("Бой с {0}", _enemy.Name);

            _coins.text = _addCoins.ToString();
            _coinsIcon.sprite = CurrencyType.Coin.GetData().Icon;
        }

        private void Continue() 
        {
            OnContinue?.Invoke();
        }

        protected override void OnDestroyScreen()
        {
            _continueBttn.onClick.RemoveAllListeners();
        }

    }
}
