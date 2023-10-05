using System;
using System.Collections.Generic;
using System.Linq;
using Test.Game;
using UnityEngine;

namespace Test.AsyncFactory
{
    [CreateAssetMenu(menuName = "Currencies/Currencies Config", fileName = "CurrenciesConfig")]
    public sealed class CurrenciesConfig : ScriptableObject
    {
        [SerializeField]
        private List<CurrencyData> _data;

        public Dictionary<CurrencyType, CurrencyData> Data = null;

        [Serializable]
        public class CurrencyData
        {
            [SerializeField]
            private CurrencyType _currency;
            [SerializeField]
            private int _defaultCount;
            [SerializeField]
            private Sprite _icon;

            public CurrencyType Currency => _currency;
            public int DefaultCount => _defaultCount;
            public Sprite Icon => _icon;
        }

        public void Init()
        {
            Data = _data.Distinct().ToDictionary(key => key.Currency, value => value);
        }
    }
}
