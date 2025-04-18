namespace Chatcloud.CodeBase.Infrastructure
{
    public interface IRequestReceiver
    {
        void OnBeginRequest();
        void ReceiveMessage(string response);

        void OnCompleteRequest();
    }
}