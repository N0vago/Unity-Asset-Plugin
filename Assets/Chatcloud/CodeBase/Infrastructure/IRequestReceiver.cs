using System;

namespace Chatcloud.CodeBase.Infrastructure
{
    /// <summary>
    /// Defines a contract for receiving backend request responses.
    /// </summary>
    public interface IRequestReceiver
    {
        /// <summary>
        /// Indicates whether the receiver is waiting for a response.
        /// </summary>
        bool IsWaitingForResponse { get; set; }

        /// <summary>
        /// Called when a request begins.
        /// </summary>
        /// <param name="message">The sent message.</param>
        void OnBeginRequest(string message);

        /// <summary>
        /// Called when a response is received.
        /// </summary>
        /// <param name="response">The received response.</param>
        void OnReceiveMessage(string response);

        /// <summary>
        /// Called when the request completes.
        /// </summary>
        void OnCompleteRequest();
    }
}