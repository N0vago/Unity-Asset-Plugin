using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Chatcloud.CodeBase.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chatcloud.CodeBase.UI
{
    public class ChatcloudWidget : MonoBehaviour
    {
        [SerializeField] private Button toggleButton;
        [SerializeField] private VerticalLayoutGroup contentField;
        [SerializeField] private ChatMessage chatMessagePrefab;
        [SerializeField] private List<RequestSample> requestSamples;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button sendButton;
        
        private const string Endpoint = "https://cc-stvx-234-001-be-2786182950.europe-central2.run.app/api/v1/chat";
        private const string Tenate = "stvx-234-001";

        public async Task SendMessageToBackend(string endpoint, string userId, string msg, Action<string> onToken,
            Action onComplete = null)
        {
            var payload = new Payload(userId, msg);
            var jsonPayload = JsonUtility.ToJson(payload);

            using var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
            {
                Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
            };

            // Важно: HttpCompletionOption.ResponseHeadersRead позволяет начинать читать поток до завершения запроса
            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (!string.IsNullOrEmpty(line))
                {
                    Debug.Log("New line");
                    onToken?.Invoke(line);
                }
            }
        }

        private void OnEnable()
        {
            sendButton.onClick.AddListener(SendRequest);
        }


        private void OnDisable()
        {
            sendButton.onClick.RemoveListener(SendRequest);
        }

        private void SendRequest()
        {
            _ = SendMessageToBackend(Endpoint, TextUtils.GenerateUserId(Tenate), inputField.text,
                DisplayMessage, Complete);
            inputField.text = string.Empty;
        }

        private void Complete()
        {
        }

        private void DisplayMessage(string message)
        {
            ChatMessage chatMessage = Instantiate(chatMessagePrefab, contentField.gameObject.transform);
            
            chatMessage.SetText(TextUtils.ConvertMarkdownToHtml(message));
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
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
    }
    [Serializable]
    public class ReplyData
    {
        public string reply;
    }
}