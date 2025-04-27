using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chatcloud.CodeBase.UI
{
    /// <summary>
    /// Represents a single chat message UI element.
    /// </summary>
    public class ChatMessage : MonoBehaviour
    {
        [Tooltip("Text component for the message content.")]
        [SerializeField] private TMP_Text messageText;

        [Tooltip("Logo image for the message.")]
        [SerializeField] private Image logo;

        [Tooltip("Background image for the message.")]
        [SerializeField] private Image background;

        // Coroutine for animating typing dots.
        private Coroutine _coroutine;

        // Parent RectTransform for layout updates.
        private RectTransform _parent;

        /// <summary>
        /// Gets or sets the logo sprite for the message.
        /// </summary>
        public Sprite Logo
        {
            get
            {
                if (logo != null)
                    return logo.sprite;
                Debug.LogError("Logo for this message wasn't set");
                return null;
            }
            set
            {
                if (logo)
                    logo.sprite = value;
            }
        }

        /// <summary>
        /// Gets or sets the background color of the message.
        /// </summary>
        public Color BackgroundColor
        {
            get => background.color;
            set => background.color = value;
        }

        /// <summary>
        /// Gets or sets the font color of the message text.
        /// </summary>
        public Color FontColor
        {
            get => messageText.color;
            set => messageText.color = value;
        }

        /// <summary>
        /// Marks the parent layout for rebuilding when enabled.
        /// </summary>
        private void OnEnable()
        {
            _parent = GetComponentInParent<RectTransform>();
            LayoutRebuilder.MarkLayoutForRebuild(_parent);
        }

        /// <summary>
        /// Displays animated typing dots for the message.
        /// </summary>
        public void ShowTypingDots()
        {
            _coroutine = StartCoroutine(AnimateDots());
        }

        /// <summary>
        /// Sets the message text and updates the layout.
        /// </summary>
        /// <param name="message">The message text to display.</param>
        public void SetText(string message)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(message);
            messageText.text = builder.ToString();

            LayoutRebuilder.ForceRebuildLayoutImmediate(_parent);
        }

        /// <summary>
        /// Animates typing dots for the message.
        /// </summary>
        private IEnumerator AnimateDots()
        {
            int dotCount = 0;
            while (true)
            {
                dotCount = (dotCount + 1) % 3;
                messageText.text = "." + new string('.', dotCount);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}