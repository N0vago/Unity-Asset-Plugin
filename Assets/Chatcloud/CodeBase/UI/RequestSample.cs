using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chatcloud.CodeBase.UI
{
    public class RequestSample : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Button button;

        public event Action<string> OnSendRequest;

        private void OnEnable()
        {
            button.onClick.AddListener(SendRequest);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(SendRequest);
        }

        private void SendRequest() => OnSendRequest?.Invoke(text.text);
    }
}