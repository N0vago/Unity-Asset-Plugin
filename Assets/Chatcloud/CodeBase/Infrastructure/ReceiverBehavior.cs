using UnityEngine;

namespace Chatcloud.CodeBase.Infrastructure
{
    public abstract class ReceiverBehavior : MonoBehaviour, IRequestReceiver
    {
        public abstract void OnBeginRequest();

        public abstract void ReceiveMessage(string response);

        public abstract void OnCompleteRequest();
    }
}