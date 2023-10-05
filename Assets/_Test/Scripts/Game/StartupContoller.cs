using Cysharp.Threading.Tasks;
using Test.AsyncFactory;
using UnityEngine;

namespace Test.Game
{
    public sealed class StartupContoller : MonoBehaviour
    {
        public static bool initialized;

#if UNITY_EDITOR
        private void Awake()
        {
            Initialize();
        }
#else
        private void Start()
        {
            Initialize();
        }
#endif

        private static void Initialize()
        {
            Debug.Log("Start loaded");
            if (initialized)
            {
                return;
            }

            App.Create();

            Application.targetFrameRate = 60;
            Application.quitting += QuittingHandler;
#if !UNITY_EDITOR
            Application.focusChanged += QuittingHandler;
#endif

            Debug.Log("End loaded");

#if UNITY_EDITOR
            LoadResources_Debug();
#else
            LoadResources().Forget();
#endif
        }

#if UNITY_EDITOR
        private static void LoadResources_Debug()
        {
            GameConfig.Init_Debug();

            initialized = true;
        }

#else
        private static async UniTask LoadResources()
        {
            await GameConfig.Init();           

            initialized = true;
        }
#endif

        private static void QuittingHandler()
        {
            if (App.Profile != null)
            {
                Save.SaveProfile();
            }
        }

#if !UNITY_EDITOR
        private static void QuittingHandler(bool focus)
        {

            if (App.Profile != null)
            {               
                if (!focus)
                {
                    Save.SaveProfile();
                }
            }
    }
#endif

    }
}
