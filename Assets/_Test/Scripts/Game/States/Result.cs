using System;
using Test.Game.UI;
using Test.States;
using Test.UI;

namespace Test.Game
{
    public class Result : BaseState
    {
        private ResultScreenController _screen;
        private Func<Enemy> _getEnemy;

        public Result(Func<Enemy> getEnemy, IStationStateSwither stateSwitcher) : base(stateSwitcher)
        {
            _getEnemy = getEnemy;
        }

        public override void Start()
        {
            var addCoins = UnityEngine.Random.Range(100, 1000);
            Currencies.Coins.add(addCoins);

            _screen = UIController.instance.OpenScreen(UIController.ScreenType.Result, _getEnemy.Invoke(), addCoins) as ResultScreenController;
            _screen.OnContinue += OnContinue;
        }

        private void OnContinue()
        {
            _stateSwitcher.SwitchState<Search>();
        }

        public override void Stop()
        {
            _screen.OnContinue -= OnContinue;
            UIController.instance.CloseCurrentScreen();
        }
    }
}
