namespace Chatcloud.CodeBase.Infrastructure
{
    public interface IRequestSender
    {
        void SendRequest(string message);
        
        IRequestReceiver Receiver { get; set; }
    }
}