using Cysharp.Threading.Tasks;

namespace Test.AsyncFactory
{
    public static class GameConfig
    {
        private static CurrenciesConfig _currencies = null;

        public static CurrenciesConfig Currencies => _currencies;

        public static async UniTask Init()
        {
            var path = string.Format(ResourcesConfig.MainPath, nameof(CurrenciesConfig));
            _currencies = await ResourcesConfig.Load<CurrenciesConfig>(path);
            _currencies.Init();
        }

#if UNITY_EDITOR
        public static void Init_Debug()
        {
            var path = string.Format(ResourcesConfig.MainPath, nameof(CurrenciesConfig));
            _currencies = ResourcesConfig.Load_Debug<CurrenciesConfig>(path);
            _currencies.Init();
        }
#endif
    }
}
