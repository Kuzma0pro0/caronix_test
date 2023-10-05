using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Game.UI
{
    public sealed class ProgressBar : MonoBehaviour 
    {
        [SerializeField]
        private Image _progress;
        [SerializeField]
        private Image _deltaProgress;

        private CancellationTokenSource _animViewSource = null;

        public float Value { get; private set; }
        public float MaxValue { get; private set; }

        public void Setup(float value, float maxValue) 
        {
            Value = value;
            MaxValue = maxValue;

            updateView(Value);
        }

        private void updateView(float value)
        {
            _progress.fillAmount = value / MaxValue;
        }

        public void OnValueChanged(float value)
        {
            _animViewSource?.Cancel();
            _animViewSource = new CancellationTokenSource();

            OnValueChangedAsync(value, _animViewSource.Token).Forget();
        }

        private async UniTask AnimateViewAsync(float valueCurrent, CancellationToken token)
        {
            updateView(valueCurrent);

            float delta = valueCurrent - Value;

            float addatsecond = delta / 14f;
            float animDuration = 0.4f;

            float currentV = Value;
            const float stepDuration = 0.03f;
            for (float i = 0; i < animDuration; i += stepDuration)
            {
                currentV += addatsecond;
                Value = currentV;
                _deltaProgress.fillAmount = currentV / MaxValue;
                await UniTask.Delay(TimeSpan.FromSeconds(stepDuration), cancellationToken: token);
            }
        }

        private async UniTask OnValueChangedAsync(float valueCurrent, CancellationToken token)
        {
            await AnimateViewAsync(valueCurrent, token);

            Value = valueCurrent;
        }

        private void OnDestroy()
        {
            _animViewSource?.Cancel();
        }

    }
}
