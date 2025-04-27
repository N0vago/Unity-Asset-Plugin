using System;

namespace Chatcloud.CodeBase.Infrastructure
{
    public interface IRequestReceiver
    {
        bool IsWaitingForResponse { get; set; }
        void OnBeginRequest(string message);
        void OnReceiveMessage(string response);

        void OnCompleteRequest();
    }
}