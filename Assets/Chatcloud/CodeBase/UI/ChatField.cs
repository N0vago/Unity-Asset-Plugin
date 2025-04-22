using Chatcloud.CodeBase.Infrastructure;
using Chatcloud.CodeBase.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Chatcloud.CodeBase.UI
{
    public class ChatField : ReceiverBehavior
    {
        [SerializeField] private ChatMessage chatMessagePrefab;
        private ChatMessage _currentChatMessage;
        
        public override void OnBeginRequest(string message)
        {
            IsWaitingForResponse = true;

            _currentChatMessage = Instantiate(chatMessagePrefab, transform);
            
            _currentChatMessage.SetText(message);
            
            _currentChatMessage = Instantiate(chatMessagePrefab, transform);
            
            _currentChatMessage.ShowTypingDots();
        }

        public override void ReceiveMessage(string response)
        {
            _currentChatMessage.SetText(TextUtils.ConvertMarkdownToTmp(response));
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }

        public override void OnCompleteRequest()
        {
            IsWaitingForResponse = false;
        }
    }
}