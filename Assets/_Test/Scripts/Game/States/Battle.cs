using System;
using Test.Game.UI;
using Test.States;
using Test.UI;

namespace Test.Game
{
    public class Battle : BaseState 
    {
        private BattleScreenController _screen;
        private Func<BattleParameters> _getBattle;

        public Battle(Func<BattleParameters> getBattle, IStationStateSwither stateSwitcher) : base(stateSwitcher)
        {
            _getBattle = getBattle;
        }

        public override void Start()
        {
            _screen = UIController.instance.OpenScreen(UIController.ScreenType.Battle, _getBattle.Invoke()) as BattleScreenController;
            _screen.OnUpdateBattle += OnUpdateBattle;
        }

        private int OnUpdateBattle() 
        {
            var battle = _getBattle.Invoke();

            var hit = UnityEngine.Random.Range(5, 11);
            battle.EnemyHealth -= hit;

            if (battle.EnemyHealth <= 0) 
            {
                _stateSwitcher.SwitchState<Result>();
            }

            return battle.EnemyHealth;
        }

        public override void Stop()
        {
            _screen.OnUpdateBattle -= OnUpdateBattle;
            UIController.instance.CloseCurrentScreen();
        }
    }
}
