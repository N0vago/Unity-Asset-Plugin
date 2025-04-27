using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chatcloud.CodeBase.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WidgetSettings", menuName = "Widget/Settings", order = 1)]
    public class WidgetSettings : ScriptableObject
    {
        [Header("Backend settings")]
        public string endpoint;
        public string tenate;
        
        [Header("Header settings")]
        public Sprite headerLogo;
        public Color headerColor = Color.black;
        public Color headerFontColor = Color.blue;
        public string headerText = "ChatcloudAI";
        
        [Header("AI chat message settings")]
        public Sprite aiMessageLogo;
        public Color aiMessageColor = Color.grey;

        [Header("User chat message settings")] 
        public Color userMessageColor = Color.blue;
        
        [Header("Request sample settings")]
        public Color requestSampleColor = Color.cyan;
        public List<string> suggestedQuestions = new List<string>();
        
        [Header("General settings")]
        public Color backgroundsColor = Color.white;
        public Color fontColor = Color.gray;
        public Sprite sendButton;

    }
}