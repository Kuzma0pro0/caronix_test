using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Test.UI
{
    public sealed class UIController : MonoBehaviour
    {
        public enum ScreenType
        {
            None,
            Hello,
            Search,
            ShowEnemy,
            Battle,
            Result
        }

        [Serializable]
        public class ScreenTypePair
        {
            [SerializeField]
            public ScreenType Type;
            [SerializeField]
            public BaseScreen Prefab;
        }

        public static UIController instance { get; private set; }

        public ScreenType CurrentScreen
        { get; private set; }

        [SerializeField]
        private List<ScreenTypePair> _screensPrefabs;
        [SerializeField]
        private RectTransform _screenContainer;

        private BaseScreen _currentScreen;

        private void Awake()
        {
            instance = this;
        }

        public BaseScreen OpenScreen(ScreenType type, params object[] param)
        {
            return OpenScreenPrivate(type, param);
        }

        private BaseScreen OpenScreenPrivate(ScreenType type, params object[] param)
        {
            if (CurrentScreen != ScreenType.None)
            {
                _currentScreen.Close();
            }

            var screen = _screensPrefabs.FirstOrDefault((p) => p.Type == type);
            if (screen == null)
            {
                throw new Exception("Unknown screen type.");
            }

            _currentScreen = Instantiate(screen.Prefab, _screenContainer);
            _currentScreen.Closing += OnScreenClosing;
            _currentScreen.Init(param);

            CurrentScreen = type;

            return _currentScreen;
        }

        public void CloseCurrentScreen()
        {
            if (_currentScreen != null)
            {
                _currentScreen.Close();
            }
        }

        private void OnScreenClosing()
        {
            CurrentScreen = ScreenType.None;
        }
    }
}
