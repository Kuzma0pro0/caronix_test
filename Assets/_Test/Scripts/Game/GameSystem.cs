using System.Collections.Generic;
using Test.States;

namespace Test.Game
{
    public class GameSystem : StateMachine 
    {
        private BattleParameters _battle;

        private void Start() 
        {
            _allStates = new List<BaseState>()
            {
                new Hello(this),
                new Search(OnEndSearch, this),
                new ShowEnemy(GetEnemy, this),
                new Battle(GetBattle, this),
                new Result(GetEnemy, this)
            };           


            if (App.Profile == null)
            {
                SwitchState<Hello>();
            }
            else 
            {
                SwitchState<Search>();
            }
        }

        private void OnEndSearch(Enemy enemy) 
        {
            _battle = new BattleParameters(enemy);
        }

        private Enemy GetEnemy() 
        {
            if (_battle == null) return default(Enemy);

            return _battle.Enemy;
        }

        private BattleParameters GetBattle()
        {
            if (_battle == null) return null;

            return _battle;
        }
    }
}
