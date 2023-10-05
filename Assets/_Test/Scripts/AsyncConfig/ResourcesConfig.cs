using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Test.AsyncFactory
{
    public static class ResourcesConfig
    {
        public static readonly string MainPath = "AsyncConfig/{0}";

        public static async UniTask<T> Load<T>(string path) where T : UnityEngine.Object
        {
            var resource = await Resources.LoadAsync<T>(path);
            return (resource as T);
        }

#if UNITY_EDITOR
        public static T Load_Debug<T>(string path) where T : UnityEngine.Object
        {
            var resource = Resources.Load<T>(path);
            return (resource as T);
        }
#endif
    }
}
