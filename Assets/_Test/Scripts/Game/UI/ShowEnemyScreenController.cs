using Test.UI;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

namespace Test.Game.UI
{
    public sealed class ShowEnemyScreenController : BaseScreen
    {
        public Action OnSearch;
        public Action OnStartBattle;

        [SerializeField]
        private Button _searchBttn;
        [SerializeField]
        private Button _battleBttn;

        [SerializeField]
        private TMP_Text _playerName;

        [SerializeField]
        private TMP_Text _enemyName;
        [SerializeField]
        private Image _enemyIcon;

        private Enemy _enemy;

        private void Start()
        {
            _searchBttn.onClick.AddListener(Search);
            _battleBttn.onClick.AddListener(Battle);          
        }

        public override void Init(params object[] param)
        {
            base.Init(param);

            if (param.Length > 0 && param[0] is Enemy)
            {
                _enemy = (Enemy)param[0];               
            }

            UpdateView();
        }

        private void UpdateView() 
        {
            _playerName.text = App.Profile.Name;

            _enemyName.text = _enemy.Name;
            _enemyIcon.sprite = _enemy.Icon;
        }

        private void Search() 
        {
            OnSearch?.Invoke();
        }

        private void Battle()
        {
            OnStartBattle?.Invoke();
        }

    }
}
