using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Chatcloud.CodeBase.Utils;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Chatcloud.CodeBase.UI
{
    public class ChatcloudWidget : MonoBehaviour
    {
        [SerializeField] private Button toggleButton;
        [SerializeField] private Transform messageContentField;
        [SerializeField] private Transform samplesContentField;
        
        [SerializeField] private ChatMessage chatMessagePrefab;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button sendButton;
        
        private ChatMessage _currentChatMessage;
        private List<RequestSample> _requestSamples = new();

        private const string Endpoint = "https://cc-stvx-234-001-be-2786182950.europe-central2.run.app/api/v1/chat";
        private const string Tenate = "stvx-234-001";

        public async Task SendMessageToBackend(string endpoint, string userId, string msg, Action<string> onToken,
            Action onComplete = null)
        {
            Payload payload = new Payload(userId, msg);
            string jsonPayload = JsonUtility.ToJson(payload);

            using HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, endpoint)
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
                    onToken?.Invoke(TextUtils.ConvertMarkdownToHtml(line.reply));
                }
            }
        }

        private void OnEnable()
        {
            _requestSamples = _requestSamples.Count == 0 ? GetRequestSamples() : _requestSamples;
            
            sendButton.onClick.AddListener(() => SendRequest(inputField.text));
            
            foreach (var requestSample in _requestSamples)
            {
                requestSample.OnSendRequest += SendRequest;
            }
        }
        
        private void OnDisable()
        {
            sendButton.onClick.RemoveAllListeners();
            foreach (var requestSample in _requestSamples)
            {
                requestSample.OnSendRequest -= SendRequest;
            }
        }

        private void SendRequest(string message)
        {
            _currentChatMessage = Instantiate(chatMessagePrefab, messageContentField.gameObject.transform);
            
            _currentChatMessage.ShowTypingDots();
            
            _ = SendMessageToBackend(Endpoint, TextUtils.GenerateUserId(Tenate), message,
                DisplayMessage, Complete);
            
            inputField.text = string.Empty;
        }

        private void Complete()
        {
        }

        private void DisplayMessage(string message)
        {
            _currentChatMessage.SetText(TextUtils.ConvertMarkdownToHtml(message));
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }

        private List<RequestSample> GetRequestSamples() => samplesContentField.GetComponentsInChildren<RequestSample>().ToList();

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