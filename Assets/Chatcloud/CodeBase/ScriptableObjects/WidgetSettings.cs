using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chatcloud.CodeBase.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject storing configuration settings for the chat widget.
    /// </summary>
    [CreateAssetMenu(fileName = "WidgetSettings", menuName = "Widget/Settings", order = 1)]
    public class WidgetSettings : ScriptableObject
    {
        [Header("Backend settings")]
        [Tooltip("Backend tenant identifier.")]
        public string tenant;

        [Tooltip("Backend API endpoint URL.")]
        public string endpoint;

        [Header("Header settings")]
        [Tooltip("Sprite for the header logo.")]
        public Sprite headerLogo;

        [Tooltip("Background color of the header.")]
        public Color headerColor = Color.black;

        [Tooltip("Font color of the header text.")]
        public Color headerFontColor = Color.blue;

        [Tooltip("Text displayed in the header.")]
        public string headerText = "ChatcloudAI";

        [Header("AI chat message settings")]
        [Tooltip("Sprite for the AI message logo.")]
        public Sprite aiMessageLogo;

        [Tooltip("Background color of AI messages.")]
        public Color aiMessageColor = Color.grey;

        [Tooltip("Font color of AI messages.")]
        public Color aiMessageFontColor = Color.black;

        [Header("User chat message settings")]
        [Tooltip("Background color of user messages.")]
        public Color userMessageColor = Color.blue;

        [Tooltip("Font color of user messages.")]
        public Color userMessageFontColor = Color.black;
        
        [Header("FAQ settings")]
        [Tooltip("Background color of FAQ sample UI elements.")]
        public Color faqSampleColor = Color.cyan;

        [Tooltip("Font color of FAQ sample UI elements.")]
        public Color faqSampleFontColor = Color.black;

        [Tooltip("List of suggested questions for request samples.")]
        public List<string> suggestedQuestions = new List<string>();

        [Header("General settings")]
        [Tooltip("Background color for general UI elements.")]
        public Color backgroundsColor = Color.white;

        [Tooltip("Input field font color")]
        public Color inputFieldFontColor = Color.gray;

        [Tooltip("Sprite for the send button.")]
        public Sprite sendButton;
    }
}