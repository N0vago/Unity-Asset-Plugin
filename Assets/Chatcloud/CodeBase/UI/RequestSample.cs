using System;
using Chatcloud.CodeBase.Infrastructure;
using Chatcloud.CodeBase.Networking;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Chatcloud.CodeBase.UI
{
    public class RequestSample : MonoBehaviour, IRequestSender
    {
        [SerializeField] private TMP_Text sampleText;
        [SerializeField] private Button button;
        [SerializeField] private Image background;

        private ReceiverBehavior _receiverBehavior;
        public Color BackgroundColor
        {
            get => background.color;
            set => background.color = value;
        }

        public Color FontColor
        {
            get => sampleText.color;
            set => sampleText.color = value;
        }
        public string Text
        {
            get => sampleText.text;
            set => sampleText.text = value;
        }

        public IRequestReceiver Receiver { get; set; }

        private void Awake()
        {
            _receiverBehavior = FindFirstObjectByType<ChatField>(FindObjectsInactive.Include);
            Receiver = _receiverBehavior;
        }
        
        public void SendRequest(string message)
        {
            if(Receiver.IsWaitingForResponse) return;
            
            _ = ChatcloudApi.SendMessageToBackend(message, Receiver.OnReceiveMessage, Receiver.OnBeginRequest, Receiver.OnCompleteRequest);
        }

        private void OnEnable()
        {
            button.onClick.AddListener(() => SendRequest(sampleText.text));
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }
        
    }
}