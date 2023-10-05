using Cysharp.Threading.Tasks;
using System;
using Test.Shared;
using Test.States;
using Test.UI;

namespace Test.Game
{
    public class Search : BaseState
    {
        private Action<Enemy> OnEndSearch;

        public Search(Action<Enemy> onEndSearch, IStationStateSwither stateSwitcher) : base(stateSwitcher)
        {
            OnEndSearch = onEndSearch;
        }

        public override void Start()
        {
            UIController.instance.OpenScreen(UIController.ScreenType.Search);
            SearchEnemy().Forget();
        }

        public override void Stop()
        {
            UIController.instance.CloseCurrentScreen();
        }

        private async UniTask SearchEnemy() 
        {
            var url = "https://randomuser.me/api/";
            var taskJson = Dowloader.DownloadJson<Response>(url);
            var json = await taskJson;

            if (json == null) 
            {
                SearchEnemy().Forget();
                return;
            }

            var taskSprite = Dowloader.DownloadSprite(json.SpriteURL);
            var sprite = await taskSprite;

            if (sprite == null)
            {
                SearchEnemy().Forget();
                return;
            }

            var enemy = new Enemy(json.Name, sprite);

            OnEndSearch?.Invoke(enemy);
            _stateSwitcher.SwitchState<ShowEnemy>();
        }   
    }
}
