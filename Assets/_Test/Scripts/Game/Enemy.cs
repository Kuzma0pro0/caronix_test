using UnityEngine;

namespace Test.Game
{
    public struct Enemy
    {
        private Sprite _icon;
        private string _name;

        public Sprite Icon => _icon;
        public string Name => _name;

        public Enemy(string name, Sprite icon)
        {
            _name = name;
            _icon = icon;
        }
    }
}
