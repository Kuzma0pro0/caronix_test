using System;
using Test.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Test.Game.UI
{
    public sealed class BattleScreenController : BaseScreen
    {
        public Func<int> OnUpdateBattle;

        [SerializeField]
        private Button _hitButton;
        [SerializeField]
        private TMP_Text _enemyName;
        [SerializeField]
        private Image _enemyIcon;
        [SerializeField]
        private ProgressBar _progressBar;

        private BattleParameters _battle;

        private void Start()
        {
            _hitButton.onClick.AddListener(OnHit);
        }

        public override void Init(params object[] param)
        {
            base.Init(param);

            if (param.Length > 0 && param[0] is BattleParameters)
            {
                _battle = (BattleParameters)param[0];
            }

            UpdateView();
        }

        private void UpdateView()
        {
            _enemyName.text = _battle.Enemy.Name;
            _enemyIcon.sprite = _battle.Enemy.Icon;

            _progressBar.Setup(_battle.EnemyHealth, _battle.EnemyHealth);
        }

        private void OnHit()
        {
            var hit = OnUpdateBattle.Invoke();
            _progressBar.OnValueChanged(hit);
        }

        protected override void OnDestroyScreen()
        {
            _hitButton.onClick.RemoveAllListeners();
        }
    }
}
