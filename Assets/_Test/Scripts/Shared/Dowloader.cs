using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Test.Shared
{
    public static class Dowloader 
    {
        public static async UniTask<T> DownloadJson<T>(string url, int timeout = 10) where T : class
        {          
            try
            {
                async UniTask<string> GetTextAsync(UnityWebRequest req)
                {
                    req.timeout = timeout;
                    var op = await req.SendWebRequest();
                    return op.downloadHandler.text;
                }

                var taskJson = GetTextAsync(UnityWebRequest.Get(url));
                var json = await taskJson;

                var result = JsonUtility.FromJson<T>(json);
                return result;
            }
            catch
            {
                Debug.LogWarning("Load json failed");
                return null;              
            }
        }

        public static async UniTask<Sprite> DownloadSprite(string url, int timeout = 10)
        {
            try
            {
                async UniTask<Texture2D> GetTextAsync(UnityWebRequest req)
                {
                    req.timeout = timeout;
                    var op = await req.SendWebRequest();
                    return ((DownloadHandlerTexture)op.downloadHandler).texture;
                }
                var request = UnityWebRequestTexture.GetTexture(url);
                var taskTexture = GetTextAsync(request);
                var texture = await taskTexture;

                var result = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero, 10f);
                return result;
            }
            catch
            {
                Debug.LogWarning("Load sprite failed");
                return null;
            }
        }

    }
}
