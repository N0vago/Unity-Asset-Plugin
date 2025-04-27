using Chatcloud.CodeBase.Infrastructure;
using Chatcloud.CodeBase.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Chatcloud.CodeBase.UI
{
    /// <summary>
    /// Manages the chat field UI, displaying user and AI messages.
    /// </summary>
    public class ChatField : ReceiverBehavior
    {
        [Tooltip("Prefab for AI chat messages.")]
        [SerializeField] private ChatMessage aiMessagePrefab;

        [Tooltip("Prefab for user chat messages.")]
        [SerializeField] private ChatMessage userMessagePrefab;

        // Current chat message being displayed.
        private ChatMessage _currentChatMessage;

        // RectTransform for layout management.
        private RectTransform _rectTransform;

        /// <summary>
        /// Called when a request begins, creating user and AI messages.
        /// </summary>
        /// <param name="message">The user message.</param>
        public override void OnBeginRequest(string message)
        {
            IsWaitingForResponse = true;

            CreateUserMessage(message);
            CreateAIMessage();
        }

        /// <summary>
        /// Updates the AI message with the received response.
        /// </summary>
        /// <param name="response">The response from the backend.</param>
        public override void OnReceiveMessage(string response)
        {
            _currentChatMessage.SetText(TextUtils.ConvertMarkdownToTmp(response));
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }

        /// <summary>
        /// Called when the request completes, resetting the waiting state.
        /// </summary>
        public override void OnCompleteRequest()
        {
            IsWaitingForResponse = false;
        }

        /// <summary>
        /// Initializes the RectTransform on start.
        /// </summary>
        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        /// <summary>
        /// Creates and positions a user message in the chat field.
        /// </summary>
        /// <param name="message">The user message to display.</param>
        private void CreateUserMessage(string message)
        {
            _currentChatMessage = Instantiate(userMessagePrefab, transform);
            _currentChatMessage.transform.localPosition = new Vector3(
                1000f - _currentChatMessage.GetComponent<RectTransform>().rect.width,
                _currentChatMessage.transform.localPosition.y,
                _currentChatMessage.transform.localPosition.z
            );
            _currentChatMessage.SetText(message);
        }

        /// <summary>
        /// Creates and positions an AI message with typing dots.
        /// </summary>
        private void CreateAIMessage()
        {
            _currentChatMessage = Instantiate(aiMessagePrefab, transform);
            _currentChatMessage.transform.localPosition = new Vector3(
                0,
                _currentChatMessage.transform.localPosition.y,
                _currentChatMessage.transform.localPosition.z
            );
            _currentChatMessage.ShowTypingDots();
        }
    }
}