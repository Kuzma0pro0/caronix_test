using Test.Game.UI;
using Test.States;
using Test.UI;

namespace Test.Game
{
    public class Hello : BaseState
    {
        private HelloScreenController _screen;

        public Hello(IStationStateSwither stateSwitcher) : base(stateSwitcher)
        {
        }

        public override void Start()
        {
            _screen = UIController.instance.OpenScreen(UIController.ScreenType.Hello) as HelloScreenController;
            _screen.OnNameSet += OnNameSet;
        }

        private void OnNameSet(string name) 
        {
            App.Profile = Profile.Create(name);
            _stateSwitcher.SwitchState<Search>();
        }

        public override void Stop()
        {
            _screen.OnNameSet -= OnNameSet;
            UIController.instance.CloseCurrentScreen();
        }
    }
}
