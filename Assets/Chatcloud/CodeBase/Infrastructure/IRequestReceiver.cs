namespace Chatcloud.CodeBase.Infrastructure
{
    public interface IRequestReceiver
    {
        bool IsWaitingForResponse { get; set; }
        void OnBeginRequest(string message);
        void ReceiveMessage(string response);

        void OnCompleteRequest();
    }
}