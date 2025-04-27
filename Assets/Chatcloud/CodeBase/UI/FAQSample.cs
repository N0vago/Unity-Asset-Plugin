using System;
using Chatcloud.CodeBase.Infrastructure;
using Chatcloud.CodeBase.Networking;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Chatcloud.CodeBase.UI
{
    /// <summary>
    /// Handles sending sample requests to the backend and updating UI elements.
    /// </summary>
    public class FAQSample : MonoBehaviour, IRequestSender
    {
        [Tooltip("Text component displaying the sample message.")]
        [SerializeField] private TMP_Text sampleText;

        [Tooltip("Button triggering the sample request.")]
        [SerializeField] private Button button;

        [Tooltip("Background image for the sample UI element.")]
        [SerializeField] private Image background;

        // Reference to the receiver behavior for handling responses.
        private ReceiverBehavior _receiverBehavior;

        /// <summary>
        /// Gets or sets the background color of the sample UI element.
        /// </summary>
        public Color BackgroundColor
        {
            get => background.color;
            set => background.color = value;
        }

        /// <summary>
        /// Gets or sets the font color of the sample text.
        /// </summary>
        public Color FontColor
        {
            get => sampleText.color;
            set => sampleText.color = value;
        }

        /// <summary>
        /// Gets or sets the text content of the sample.
        /// </summary>
        public string Text
        {
            get => sampleText.text;
            set => sampleText.text = value;
        }

        /// <summary>
        /// Gets or sets the receiver handling the backend response.
        /// </summary>
        public IRequestReceiver Receiver { get; set; }

        /// <summary>
        /// Initializes the receiver on awake.
        /// </summary>
        private void Awake()
        {
            _receiverBehavior = FindFirstObjectByType<ChatField>(FindObjectsInactive.Include);
            Receiver = _receiverBehavior;
        }

        /// <summary>
        /// Sends a request to the backend with the provided message.
        /// </summary>
        /// <param name="message">The message to send.</param>
        public void SendRequest(string message)
        {
            if (Receiver.IsWaitingForResponse) return;

            _ = ChatcloudApi.SendMessageToBackend(message, Receiver.OnReceiveMessage, Receiver.OnBeginRequest, Receiver.OnCompleteRequest);
        }

        /// <summary>
        /// Adds the button click listener on enable.
        /// </summary>
        private void OnEnable()
        {
            button.onClick.AddListener(() => SendRequest(sampleText.text));
        }

        /// <summary>
        /// Removes the button click listener on disable.
        /// </summary>
        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}