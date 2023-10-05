using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Test.Game;
using UnityEditor;
using UnityEngine;

namespace Test
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Profile
    {
        [JsonProperty]
        private string name;
        [JsonProperty]
        private Dictionary<CurrencyType, double> currencyList = new Dictionary<CurrencyType, double>();
        [JsonProperty]
        private DateTime creationTime = DateTime.UtcNow;
        [JsonProperty]
        private DateTime lastSaveTime;

        public static Profile Create(string name)
        {
            return new Profile(true, name);
        }
        public Profile()
            : this(false)
        { }

        private Profile(bool initAsNew, string name = null)
        {
            if (initAsNew)
            {
                this.name = name;
            }
        }

        public DateTime CreationTime
        {
            get => creationTime;
        }
        public DateTime LastSaveTime
        {
            get => lastSaveTime;
            set => lastSaveTime = value;
        }
        public string Name
        {
            get => name;
        }
        public Dictionary<CurrencyType, double> CurrencyList
        {
            get => currencyList;
            set => currencyList = value;
        }
    }

#if UNITY_EDITOR
    public static class TycoonMenu
    {
        [MenuItem("TEST/Profile/Reset")]
        public static void ResetProfile()
        {
            Directory.Delete(App.GetAppDirectory(), true);
        }

        [MenuItem("TEST/Profile/Reset", validate = true)]
        public static bool ResetProfileValidate()
        {
            return !Application.isPlaying;
        }
    }
#endif
}

