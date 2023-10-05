using System;
using UnityEngine;

namespace Test.UI
{
    public class BaseScreen : MonoBehaviour
    {
        public event Action Closing;
        public event Action Closed;

        public virtual void Init(params object[] param)
        { }

        public virtual void Update() 
        { }

        public void Close()
        {
            ClosePrivate();
        }

        private void ClosePrivate()
        {
            Closing?.Invoke();
            Closing = null;
            BeforeClosed();

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            Closed?.Invoke();
            Closed = null;

            OnDestroyScreen();
        }

        protected virtual void OnDestroyScreen()
        { }

        protected virtual void BeforeClosed()
        { }
    }
}
