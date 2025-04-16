using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chatcloud.CodeBase.UI
{
    public class ChatMessage : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private ContentSizeFitter _contentSizeFitter;
        

        public void SetText(string message)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(message);
            text.text = builder.ToString();
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }
    }
}