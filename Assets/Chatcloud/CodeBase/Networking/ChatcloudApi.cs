using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Chatcloud.CodeBase.ScriptableObjects;
using Chatcloud.CodeBase.Utils;
using UnityEngine;
using Random = System.Random;

namespace Chatcloud.CodeBase.Networking
{
    public static class ChatcloudApi
    {
        private static BackendData _backendData;
        private const string Key = "user_id";

        static ChatcloudApi()
        {
            _backendData = Resources.Load<BackendData>("BackendData");
        }
        
        public static async Task SendMessageToBackend(string msg, Action<string> onToken, Action<string> onBegin = null,
            Action onComplete = null)
        {
            onBegin?.Invoke(msg);
            
            Payload payload = new Payload(GenerateUserId(_backendData.tenate), msg);
            string jsonPayload = JsonUtility.ToJson(payload);

            using HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _backendData.endpoint)
            {
                Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
            };
            
            using HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            await using Stream stream = await response.Content.ReadAsStreamAsync();
            using StreamReader reader = new StreamReader(stream);

            while (!reader.EndOfStream)
            {
                string json = await reader.ReadLineAsync();

                ReplyData line = JsonUtility.FromJson<ReplyData>(json);
                
                if (!string.IsNullOrEmpty(line.reply))
                {
                    onToken?.Invoke(TextUtils.ConvertMarkdownToTmp(line.reply));
                }
            }
            
            onComplete?.Invoke();
        }

        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return result.ToString();
        }

        private static string GenerateUserId(string tenantId)
        {
            string userId;
            int timestamp = (int)(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            string rnd = GenerateRandomString(10);
            
            if (string.IsNullOrEmpty(PlayerPrefs.GetString(Key)))
            {
                userId = $"{tenantId}_unity_{timestamp}_{rnd}";
                PlayerPrefs.SetString(Key, userId);
            }
            else
            {
                userId = PlayerPrefs.GetString(Key);
            }
            
            Debug.Log(userId);
            return userId;
        }

        [Serializable]
        private class Payload
        {
            public string user_id;
            public string message;

            public Payload(string userId, string message)
            {
                this.user_id = userId;
                this.message = message;
            }
        }

        [Serializable]
        private class ReplyData
        {
            public string reply;
        }
    }
}