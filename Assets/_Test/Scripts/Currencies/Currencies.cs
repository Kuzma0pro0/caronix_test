using System.Collections.Generic;
using System.Linq;
using Test.AsyncFactory;
using Test.Shared;
using UnityEngine;

namespace Test.Game
{
    public static class Currencies
    {
        private static IReadOnlyDictionary<CurrencyType, Currency> _currencies = null;

        public static IReadOnlyDictionary<CurrencyType, Currency> currencies
        {
            get
            {
                if (_currencies == null)
                {
                    _currencies = Load();
                }

                return _currencies;
            }
        }

        private static IReadOnlyList<CurrencyType> _currenciesType => Toolbox.EnumValues<CurrencyType>().ToList();

        private static IReadOnlyDictionary<CurrencyType, Currency> Load()
        {
            Dictionary<CurrencyType, Currency> tempCurrency = new Dictionary<CurrencyType, Currency>();
            var config = GameConfig.Currencies.Data;

            foreach (var currency in _currenciesType)
            {
                if (config.ContainsKey(currency))
                {
                    switch (currency)
                    {
                        default:
                            tempCurrency.Add(currency, new Currency(currency, config[currency].DefaultCount));
                            break;
                    }
                }
                else
                {
                    tempCurrency.Add(currency, new Currency(currency, 0));
                }
            }

            return tempCurrency;
        }

        public static Currency Coins => currencies[CurrencyType.Coin];
    }
}
