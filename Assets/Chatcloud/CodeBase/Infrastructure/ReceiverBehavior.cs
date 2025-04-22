using UnityEngine;

namespace Chatcloud.CodeBase.Infrastructure
{
    public abstract class ReceiverBehavior : MonoBehaviour, IRequestReceiver
    {
        public bool IsWaitingForResponse { get; set; }
        public abstract void OnBeginRequest(string message);

        public abstract void ReceiveMessage(string response);

        public abstract void OnCompleteRequest();
    }
}