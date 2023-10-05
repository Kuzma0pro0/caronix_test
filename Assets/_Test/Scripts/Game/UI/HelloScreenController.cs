using System;
using Test.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Game.UI
{
    public sealed class HelloScreenController : BaseScreen
    {
        public Action<string> OnNameSet;

        [SerializeField]
        private TMP_InputField _inputField;
        [SerializeField]
        private Button _bttn;

        private string _name;

        private void Start()
        {
            _inputField.onValueChanged.AddListener(NameSet);
            _bttn.onClick.AddListener(Continue);
        }

        private void NameSet(string name) 
        {
            _name = name;
        }

        private void Continue() 
        {
            if (string.IsNullOrWhiteSpace(_name)) return;

            OnNameSet?.Invoke(_name);
        }

        protected override void OnDestroyScreen() 
        {
            _inputField.onValueChanged.RemoveAllListeners();
            _bttn.onClick.RemoveAllListeners();
        }
    }
}
