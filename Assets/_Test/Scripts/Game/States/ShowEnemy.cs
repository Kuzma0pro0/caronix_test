using System;
using Test.Game.UI;
using Test.States;
using Test.UI;

namespace Test.Game
{
    public class ShowEnemy : BaseState
    {
        private ShowEnemyScreenController _screen;
        private Func<Enemy> _getEnemy;

        public ShowEnemy(Func<Enemy> getEnemy, IStationStateSwither stateSwitcher) : base(stateSwitcher)
        {
            _getEnemy = getEnemy;
        }

        public override void Start()
        {
            _screen = UIController.instance.OpenScreen(UIController.ScreenType.ShowEnemy, _getEnemy.Invoke()) as ShowEnemyScreenController;
            _screen.OnSearch += OnSearch;
            _screen.OnStartBattle += OnStartBattle;
        }

        private void OnSearch() 
        {
            _stateSwitcher.SwitchState<Search>();
        }

        private void OnStartBattle()
        {
            _stateSwitcher.SwitchState<Battle>();
        }

        public override void Stop()
        {
            _screen.OnSearch -= OnSearch;
            _screen.OnStartBattle -= OnStartBattle;
            UIController.instance.CloseCurrentScreen();
        }
    }
}
