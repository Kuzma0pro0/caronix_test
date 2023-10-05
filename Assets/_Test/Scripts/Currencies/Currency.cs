using System;
using System.Collections.Generic;

namespace Test.Game
{
    public class Currency
    {
        public Action<CurrencyType, double> ValueChanged;
        public Action<CurrencyType, double, string, bool> Added;
        public Action<CurrencyType, double, string> Spent;

        private readonly CurrencyType _type;
        private readonly double _defaultValue;

        private Dictionary<CurrencyType, double> CurrencyList
        {
            get => App.Profile.CurrencyList;
            set
            {
                App.Profile.CurrencyList = value;
            }
        }

        public Currency(CurrencyType type, long defaultValue)
        {
            _type = type;
            _defaultValue = defaultValue;
        }

        public double Value
        {
            get
            {
                if (!_value.HasValue)
                {
                    if (CurrencyList.ContainsKey(_type))
                    {
                        _value = CurrencyList[_type];
                    }
                    else
                    {
                        CurrencyList.Add(_type, _defaultValue);
                        _value = _defaultValue;
                    }
                }

                return _value.Value;
            }

            set
            {
                _value = value;
                if (CurrencyList.ContainsKey(_type))
                {
                    CurrencyList[_type] = value;
                }
                else
                {
                    CurrencyList.Add(_type, value);
                }

                ValueChanged?.Invoke(_type, value);
            }
        }

        private double? _value = null;

        public void add(double count, string src = "", bool purchased = false)
        {
            Value += count;
            Added?.Invoke(_type, count, src, purchased);
        }

        public void spend(double count, string src = "")
        {
            Value -= count;
            Spent?.Invoke(_type, count, src);
        }
    }
}
