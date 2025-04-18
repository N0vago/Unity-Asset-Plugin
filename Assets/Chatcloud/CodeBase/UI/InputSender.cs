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

        public void SendRequest(string message)
        {
            _ = ChatcloudApi.SendMessageToBackend(message, Receiver.ReceiveMessage, Receiver.OnBeginRequest,
                Receiver.OnCompleteRequest);

            _inputField.text = string.Empty;
        }
    }
}