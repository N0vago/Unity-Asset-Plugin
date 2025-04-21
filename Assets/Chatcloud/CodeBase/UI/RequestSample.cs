using System;
using Chatcloud.CodeBase.Infrastructure;
using Chatcloud.CodeBase.Networking;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chatcloud.CodeBase.UI
{
    public class RequestSample : MonoBehaviour, IRequestSender
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Button button;
        [SerializeField] private ReceiverBehavior receiverBehavior;

        public IRequestReceiver Receiver { get; set; }

        private void Awake()
        {
            Receiver = receiverBehavior;
        }

        public void SendRequest(string message)
        {
            if(Receiver.IsWaitingForResponse) return;
            
            _ = ChatcloudApi.SendMessageToBackend(message, Receiver.ReceiveMessage, Receiver.OnBeginRequest, Receiver.OnCompleteRequest);
        }

        private void OnEnable()
        {
            button.onClick.AddListener(() => SendRequest(text.text));
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }
        
    }
}