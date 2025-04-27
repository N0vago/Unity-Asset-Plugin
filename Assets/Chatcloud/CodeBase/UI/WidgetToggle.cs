using System;
using UnityEngine;
using UnityEngine.UI;

namespace Chatcloud.CodeBase.UI
{
    /// <summary>
    /// Toggles the visibility of a widget UI element.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class WidgetToggle : MonoBehaviour
    {
        [Tooltip("Prefab of the widget to toggle.")]
        [SerializeField] private GameObject prefab;

        [Tooltip("Sprite for the button when the widget is enabled.")]
        [SerializeField] private Sprite enabledSprite;

        [Tooltip("Sprite for the button when the widget is disabled.")]
        [SerializeField] private Sprite disabledSprite;

        // Reference to the button component.
        private Button _button;

        // Reference to the image component.
        private Image _image;

        /// <summary>
        /// Initializes button and image components on awake.
        /// </summary>
        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
        }

        /// <summary>
        /// Adds the toggle listener on enable.
        /// </summary>
        private void OnEnable()
        {
            _button.onClick.AddListener(ToggleWidget);
        }

        /// <summary>
        /// Removes the toggle listener on disable.
        /// </summary>
        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        /// <summary>
        /// Sets the initial sprite based on the widget's state.
        /// </summary>
        private void Start()
        {
            _image.sprite = prefab.activeInHierarchy ? enabledSprite : disabledSprite;
        }

        /// <summary>
        /// Toggles the widget's active state and updates the button sprite.
        /// </summary>
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