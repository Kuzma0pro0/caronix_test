using Test.AsyncFactory;

namespace Test.Game
{
    public static class CurrencyExtension
    {
        public static Currency GetCurrency(this CurrencyType type)
        {
            return Currencies.currencies[type];
        }

        public static CurrenciesConfig.CurrencyData GetData(this CurrencyType type)
        {
            return GameConfig.Currencies.Data[type];
        }
    }
}
