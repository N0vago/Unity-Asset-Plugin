namespace Chatcloud.CodeBase.Infrastructure
{
    /// <summary>
    /// Defines a contract for sending requests to the backend.
    /// </summary>
    public interface IRequestSender
    {
        /// <summary>
        /// Sends a request with the specified message.
        /// </summary>
        /// <param name="message">The message to send.</param>
        void SendRequest(string message);

        /// <summary>
        /// Gets or sets the receiver handling the response.
        /// </summary>
        IRequestReceiver Receiver { get; set; }
    }
}