using Chatcloud.CodeBase.Infrastructure;
using Chatcloud.CodeBase.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Chatcloud.CodeBase.UI
{
    public class ChatField : ReceiverBehavior
    {
        [SerializeField] private ChatMessage aiMessagePrefab;
        [SerializeField] private ChatMessage userMessagePrefab;
        
        private ChatMessage _currentChatMessage;

        private RectTransform _rectTransform;
        
        public override void OnBeginRequest(string message)
        {
            IsWaitingForResponse = true;

            CreateUserMessage(message);

            CreateAIMessage();
        }

        public override void OnReceiveMessage(string response)
        {
            _currentChatMessage.SetText(TextUtils.ConvertMarkdownToTmp(response));
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }

        public override void OnCompleteRequest()
        {
            IsWaitingForResponse = false;
        }

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void CreateUserMessage(string message)
        {
            _currentChatMessage = Instantiate(userMessagePrefab, transform);
            
            _currentChatMessage.transform.localPosition = new Vector3(1000f - _currentChatMessage.GetComponent<RectTransform>().rect.width, _currentChatMessage.transform.localPosition.y, _currentChatMessage.transform.localPosition.z);

            _currentChatMessage.SetText(message);
        }

        private void CreateAIMessage()
        {
            _currentChatMessage = Instantiate(aiMessagePrefab, transform);
            
            _currentChatMessage.transform.localPosition = new Vector3(0, _currentChatMessage.transform.localPosition.y, _currentChatMessage.transform.localPosition.z);

            _currentChatMessage.ShowTypingDots();
        }
    }
}