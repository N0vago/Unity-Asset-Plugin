using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chatcloud.CodeBase.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WidgetSettings", menuName = "Widget/Settings", order = 1)]
    public class WidgetSettings : ScriptableObject
    {
        [Header("Header settings")]
        public Sprite headerLogo;
        public Color headerColor = Color.black;
        public Color headerFontColor = Color.blue;
        public string headerText = "ChatcloudAI";
        
        [Header("Chat message settings")]
        public Sprite messageLogo;
        public Color messageColor = Color.grey; 
        
        [Header("Request sample settings")]
        public Color requestSampleColor = Color.cyan;
        public List<string> suggestedQuestions = new List<string>();
        
        [Header("General settings")]
        public Color backgroundsColor = Color.white;
        public Color fontColor = Color.gray;
        public Sprite sendButton;

    }
}