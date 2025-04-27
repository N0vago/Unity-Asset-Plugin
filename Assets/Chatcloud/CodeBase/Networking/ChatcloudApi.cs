using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Chatcloud.CodeBase.ScriptableObjects;
using Chatcloud.CodeBase.Utils;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

namespace Chatcloud.CodeBase.Networking
{
    /// <summary>
    /// Handles communication with the Chatcloud backend API.
    /// </summary>
    public static class ChatcloudApi
    {
        // Tenant identifier for the backend.
        private static readonly string Tenant;

        // Backend API endpoint URL.
        private static readonly string Endpoint;

        // Key for storing user ID in PlayerPrefs.
        private const string Key = "user_id";

        /// <summary>
        /// Initializes tenant and endpoint from WidgetSettings.
        /// </summary>
        static ChatcloudApi()
        {
            var settings = AssetDatabase.LoadAssetAtPath<WidgetSettings>("Assets/Chatcloud/WidgetSettings.asset");
            Tenant = settings.tenant;
            Endpoint = settings.endpoint;
        }

        /// <summary>
        /// Sends a message to the backend and processes the response.
        /// </summary>
        /// <param name="msg">The message to send.</param>
        /// <param name="onToken">Callback for receiving response tokens.</param>
        /// <param name="onBegin">Callback for when the request begins.</param>
        /// <param name="onComplete">Callback for when the request completes.</param>
        public static async Task SendMessageToBackend(string msg, Action<string> onToken, Action<string> onBegin = null,
            Action onComplete = null)
        {
            if (string.IsNullOrEmpty(Tenant) || string.IsNullOrEmpty(Endpoint))
            {
                Debug.LogError("Tenate or endpoint wasn't set");
                return;
            }

            onBegin?.Invoke(msg);

            Payload payload = new Payload(GenerateUserId(Tenant), msg);
            string jsonPayload = JsonUtility.ToJson(payload);

            using HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Endpoint)
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

        /// <summary>
        /// Generates a random string of specified length.
        /// </summary>
        /// <param name="length">Length of the random string.</param>
        /// <returns>The generated random string.</returns>
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

        /// <summary>
        /// Generates or retrieves a unique user ID.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns>The generated or stored user ID.</returns>
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

        /// <summary>
        /// Represents the payload sent to the backend.
        /// </summary>
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

        /// <summary>
        /// Represents the response data from the backend.
        /// </summary>
        [Serializable]
        private class ReplyData
        {
            public string reply;
        }
    }
}