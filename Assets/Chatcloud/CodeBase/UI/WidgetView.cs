using System.Collections.Generic;
using System.Linq;
using Chatcloud.CodeBase.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chatcloud.CodeBase.UI
{
    public class WidgetView : MonoBehaviour
    {
        [SerializeField] private WidgetSettings settings;
        
        [SerializeField] private RequestSample requestSamplePrefab;
        [SerializeField] private ChatMessage chatMessagePrefab;
        
        [SerializeField] private Image headerLogo;
        [SerializeField] private TMP_Text headerText;
        [SerializeField] private Image headerBackgroundImage;
        
        [SerializeField] private List<Image> backgroundImages;
        [SerializeField] private List<TMP_Text> widgetTexts;
        [SerializeField] private Image sendButton;

        [SerializeField] private Transform requestSamplesField;
        
        public void ApplySettings()
        {
            if (headerLogo != null)
                headerLogo.sprite = settings.headerLogo;

            if (chatMessagePrefab != null)
            {
                chatMessagePrefab.FontColor = settings.fontColor;
                chatMessagePrefab.BackgroundColor = settings.messageColor;
                chatMessagePrefab.Logo = settings.messageLogo;
            }

            if (requestSamplePrefab != null)
            {
                requestSamplePrefab.BackgroundColor = settings.requestSampleColor;
                requestSamplePrefab.FontColor = settings.fontColor;
            }

            if (backgroundImages != null)
                backgroundImages.ForEach(image => image.color = settings.backgroundsColor);

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
            
            if(widgetTexts != null)
                widgetTexts.ForEach( text => text.color = settings.fontColor);
                

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