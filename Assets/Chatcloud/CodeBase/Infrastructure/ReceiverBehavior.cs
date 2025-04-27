using System;
using UnityEngine;

namespace Chatcloud.CodeBase.Infrastructure
{
    /// <summary>
    /// Abstract base class for handling backend request responses.
    /// </summary>
    public abstract class ReceiverBehavior : MonoBehaviour, IRequestReceiver
    {
        /// <summary>
        /// Indicates whether the receiver is waiting for a response.
        /// </summary>
        public bool IsWaitingForResponse { get; set; }

        /// <summary>
        /// Called when a request begins.
        /// </summary>
        /// <param name="message">The sent message.</param>
        public abstract void OnBeginRequest(string message);

        /// <summary>
        /// Called when a response is received.
        /// </summary>
        /// <param name="response">The received response.</param>
        public abstract void OnReceiveMessage(string response);

        /// <summary>
        /// Called when the request completes.
        /// </summary>
        public abstract void OnCompleteRequest();
    }
}