using System;
using UnityEngine;
using UnityEngine.UI;

namespace Chatcloud.CodeBase.UI
{
    [RequireComponent(typeof(Button))]
    public class WidgetToggle : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Sprite enabledSprite;
        [SerializeField] private Sprite disabledSprite;

        private Button _button;
        private Image _image;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ToggleWidget);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void Start() => _image.sprite = prefab.activeInHierarchy ? enabledSprite : disabledSprite;

        private void ToggleWidget()
        {
            if (prefab.activeInHierarchy)
            {
                prefab.SetActive(false);
                _image.sprite = disabledSprite;
            }
            else
            {
                prefab.SetActive(true);
                _image.sprite = enabledSprite;
            }
        }
    }
}