using System.Collections.Generic;
using System.Linq;
using Chatcloud.CodeBase.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Chatcloud.CodeBase.UI
{
    public class WidgetView : MonoBehaviour
    {
        [SerializeField] private WidgetSettings settings;
        
        [Header("Header setup")]
        [SerializeField] private Image headerLogo;
        [SerializeField] private TMP_Text headerText;
        [SerializeField] private Image headerBackgroundImage;
        
        [Header("Chat messages setup")]
        [SerializeField] private ChatMessage aiChatMessagePrefab;
        [SerializeField] private ChatMessage userChatMessagePrefab;
        
        [Header("Request samples setup")]
        [SerializeField] private RequestSample requestSamplePrefab;
        [SerializeField] private Transform requestSamplesField;
        
        [Header("Other")]
        [SerializeField] private List<Image> otherImages;
        [SerializeField] private List<TMP_Text> otherTexts;
        [SerializeField] private Image sendButton;
        
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
            {
                sendButton.sprite = settings.sendButton;
            }
            
            if(otherTexts != null)
                otherTexts.ForEach( text => text.color = settings.fontColor);
                

            foreach (var question in settings.suggestedQuestions)
            {
                if (requestSamplesField.GetComponentsInChildren<RequestSample>()
                    .Any(sample => sample.Text == question)) continue;
                RequestSample sample = Instantiate(requestSamplePrefab, requestSamplesField);
                sample.Text = question;
            }
        }

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