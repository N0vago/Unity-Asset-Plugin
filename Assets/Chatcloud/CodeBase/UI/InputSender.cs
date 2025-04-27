using System;
using Chatcloud.CodeBase.Infrastructure;
using Chatcloud.CodeBase.Networking;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chatcloud.CodeBase.UI
{
    public class InputSender : MonoBehaviour, IRequestSender
    {
        [SerializeField] private Button button;
        [SerializeField] private ReceiverBehavior receiverBehavior;

        private TMP_InputField _inputField;
        public IRequestReceiver Receiver { get; set; }

        private void Awake()
        {
            _inputField = GetComponent<TMP_InputField>();
            Receiver = receiverBehavior;
        }

        private void OnEnable()
        {
            button.onClick.AddListener(() => SendRequest(_inputField.text));
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }

        private void Update()
        {
            if (Receiver.IsWaitingForResponse) _inputField.interactable = false;
            _inputField.interactable = true;
        }

        public async void SendRequest(string message)
        {
            _inputField.text = string.Empty;
            
            await ChatcloudApi.SendMessageToBackend(message, Receiver.OnReceiveMessage, Receiver.OnBeginRequest,
                Receiver.OnCompleteRequest);
        }
    }
}