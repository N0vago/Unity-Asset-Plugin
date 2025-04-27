using System;
using Chatcloud.CodeBase.Infrastructure;
using Chatcloud.CodeBase.Networking;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chatcloud.CodeBase.UI
{
    /// <summary>
    /// Handles sending user input to the backend.
    /// </summary>
    public class InputSender : MonoBehaviour, IRequestSender
    {
        [Tooltip("Button triggering the send action.")]
        [SerializeField] private Button button;

        [Tooltip("Receiver behavior handling the backend response.")]
        [SerializeField] private ReceiverBehavior receiverBehavior;

        // Input field for user messages.
        private TMP_InputField _inputField;

        /// <summary>
        /// Gets or sets the receiver handling the backend response.
        /// </summary>
        public IRequestReceiver Receiver { get; set; }

        /// <summary>
        /// Initializes the input field and receiver on awake.
        /// </summary>
        private void Awake()
        {
            _inputField = GetComponent<TMP_InputField>();
            Receiver = receiverBehavior;
        }

        /// <summary>
        /// Adds the button click listener on enable.
        /// </summary>
        private void OnEnable()
        {
            button.onClick.AddListener(() => SendRequest(_inputField.text));
        }

        /// <summary>
        /// Removes the button click listener on disable.
        /// </summary>
        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }

        /// <summary>
        /// Updates the input field interactability based on response state.
        /// </summary>
        private void Update()
        {
            _inputField.interactable = !Receiver.IsWaitingForResponse;
        }

        /// <summary>
        /// Sends the user input to the backend and clears the input field.
        /// </summary>
        /// <param name="message">The message to send.</param>
        public async void SendRequest(string message)
        {
            _inputField.text = string.Empty;
            await ChatcloudApi.SendMessageToBackend(message, Receiver.OnReceiveMessage, Receiver.OnBeginRequest,
                Receiver.OnCompleteRequest);
        }
    }
}