using System.Collections.Generic;
using System.Linq;
using Chatcloud.CodeBase.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Chatcloud.CodeBase.UI
{
    /// <summary>
    /// Manages the visual setup of the chat widget UI.
    /// </summary>
    public class WidgetView : MonoBehaviour
    {
        [Tooltip("Settings for the widget's appearance and behavior.")]
        [SerializeField] private WidgetSettings settings;

        [Header("Header setup")]
        [Tooltip("Logo image for the header.")]
        [SerializeField] private Image headerLogo;

        [Tooltip("Text displayed in the header.")]
        [SerializeField] private TMP_Text headerText;

        [Tooltip("Background image for the header.")]
        [SerializeField] private Image headerBackgroundImage;

        [Header("Chat messages setup")]
        [Tooltip("Prefab for AI chat messages.")]
        [SerializeField] private ChatMessage aiChatMessagePrefab;

        [Tooltip("Prefab for user chat messages.")]
        [SerializeField] private ChatMessage userChatMessagePrefab;

        [Header("Request samples setup")]
        [Tooltip("Prefab for request sample UI elements.")]
        [SerializeField] private RequestSample requestSamplePrefab;

        [Tooltip("Parent transform for request sample UI elements.")]
        [SerializeField] private Transform requestSamplesField;

        [Header("Other")]
        [Tooltip("List of additional images for styling.")]
        [SerializeField] private List<Image> otherImages;

        [Tooltip("List of additional text elements for styling.")]
        [SerializeField] private List<TMP_Text> otherTexts;

        [Tooltip("Image for the send button.")]
        [SerializeField] private Image sendButton;

        /// <summary>
        /// Applies the widget settings to update the UI appearance.
        /// </summary>
        public void ApplySettings()
        {
            if (headerLogo != null)
                headerLogo.sprite = settings.headerLogo;

            if (aiChatMessagePrefab != null)
            {
                aiChatMessagePrefab.FontColor = settings.fontColor;
                aiChatMessagePrefab.BackgroundColor = settings.aiMessageColor;
                aiChatMessagePrefab.Logo = settings.aiMessageLogo;
            }

            if (userChatMessagePrefab != null)
            {
                userChatMessagePrefab.FontColor = settings.fontColor;
                userChatMessagePrefab.BackgroundColor = settings.userMessageColor;
            }

            if (requestSamplePrefab != null)
            {
                requestSamplePrefab.BackgroundColor = settings.requestSampleColor;
                requestSamplePrefab.FontColor = settings.fontColor;
            }

            if (otherImages != null)
                otherImages.ForEach(image => image.color = settings.backgroundsColor);

            if (headerBackgroundImage != null)
                headerBackgroundImage.color = settings.headerColor;

            if (headerText != null)
            {
                headerText.text = settings.headerText;
                headerText.color = settings.headerFontColor;
            }

            if (sendButton != null)
                sendButton.sprite = settings.sendButton;

            if (otherTexts != null)
                otherTexts.ForEach(text => text.color = settings.fontColor);

            foreach (var question in settings.suggestedQuestions)
            {
                if (requestSamplesField.GetComponentsInChildren<RequestSample>()
                    .Any(sample => sample.Text == question)) continue;
                RequestSample sample = Instantiate(requestSamplePrefab, requestSamplesField);
                sample.Text = question;
            }
        }

        /// <summary>
        /// Clears all request samples from the UI.
        /// </summary>
        public void ClearSamples()
        {
            for (int i = requestSamplesField.childCount - 1; i >= 0; i--)
            {
                Transform child = requestSamplesField.GetChild(i);
                DestroyImmediate(child.gameObject);
            }
        }
    }
}