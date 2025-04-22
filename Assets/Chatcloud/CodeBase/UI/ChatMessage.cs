using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chatcloud.CodeBase.UI
{
    public class ChatMessage : MonoBehaviour
    {
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private Image logo;
        [SerializeField] private Image background;

        private Coroutine _coroutine;
        private RectTransform _parent;

        public Sprite Logo
        {
            get => logo.sprite;
            set => logo.sprite = value;
        }

        public Color BackgroundColor
        {
            get => background.color;
            set => background.color = value;
        }

        public Color FontColor
        {
            get => messageText.color;
            set => messageText.color = value;
        }

        private void OnEnable()
        {
            _parent = GetComponentInParent<RectTransform>();
            
            LayoutRebuilder.MarkLayoutForRebuild(_parent);
        }

        public void ShowTypingDots()
        {
            _coroutine = StartCoroutine(AnimateDots());
        }
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