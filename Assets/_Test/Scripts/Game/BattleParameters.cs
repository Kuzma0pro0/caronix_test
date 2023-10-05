namespace Test.Game
{
    public class BattleParameters
    {
        private Enemy _enemy;
        private int _enemyHealth;

        public BattleParameters(Enemy enemy) 
        {
            _enemy = enemy;
            _enemyHealth = UnityEngine.Random.Range(50, 101);
        }

        public int EnemyHealth { get => _enemyHealth; set => _enemyHealth = value; }
        public Enemy Enemy => _enemy;
    }
}
