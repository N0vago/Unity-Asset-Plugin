using Chatcloud.CodeBase.ScriptableObjects;
using Chatcloud.CodeBase.UI;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Chatcloud.CodeBase.Editor
{
    /// <summary>
    /// Custom editor window for configuring WidgetSettings.
    /// </summary>
    public class WidgetSettingEditor : EditorWindow
    {
        // Reference to the target WidgetView.
        private WidgetView _targetWidget;

        // Widget settings asset.
        private WidgetSettings _settings;

        // Serialized object for WidgetSettings.
        private SerializedObject _serializedSettings;

        // Serialized properties for WidgetSettings fields.
        private SerializedProperty _tenateProp;
        private SerializedProperty _endpointProp;
        
        private SerializedProperty _headerLogoProp;
        private SerializedProperty _headerColorProp;
        private SerializedProperty _headerFontColorProp;
        private SerializedProperty _headerTextProp;
        
        private SerializedProperty _aiMessageLogoProp;
        private SerializedProperty _aiMessageColorProp;
        private SerializedProperty _aiMessageFontColorProp;
        
        private SerializedProperty _userMessageColorProp;
        private SerializedProperty _userMessageFontColorProp;
        
        private SerializedProperty _faqSampleColorProp;
        private SerializedProperty _faqSampleFontColorProp;
        private SerializedProperty _suggestedQuestionsProp;
        
        private SerializedProperty _backgroundColorProp;
        private SerializedProperty _inputFieldFontColorProp;
        private SerializedProperty _sendButtonProp;

        /// <summary>
        /// Opens the WidgetSettingEditor window.
        /// </summary>
        [MenuItem("Chatcloud/Settings")]
        public static void ShowWindow()
        {
            GetWindow<WidgetSettingEditor>("Chat settings");
        }

        /// <summary>
        /// Initializes the editor window and loads WidgetSettings.
        /// </summary>
        private void OnEnable()
        {
            _settings = AssetDatabase.LoadAssetAtPath<WidgetSettings>("Assets/Chatcloud/WidgetSettings.asset");
            if (_settings == null)
            {
                _settings = CreateInstance<WidgetSettings>();
                AssetDatabase.CreateAsset(_settings, "Assets/WidgetSettings.asset");
                AssetDatabase.SaveAssets();
            }

            _serializedSettings = new SerializedObject(_settings);

            _tenateProp = _serializedSettings.FindProperty("tenant");
            _endpointProp = _serializedSettings.FindProperty("endpoint");
            
            _headerLogoProp = _serializedSettings.FindProperty("headerLogo");
            _headerColorProp = _serializedSettings.FindProperty("headerColor");
            _headerFontColorProp = _serializedSettings.FindProperty("headerFontColor");
            _headerTextProp = _serializedSettings.FindProperty("headerText");
            
            _aiMessageLogoProp = _serializedSettings.FindProperty("aiMessageLogo");
            _aiMessageColorProp = _serializedSettings.FindProperty("aiMessageColor");
            _aiMessageFontColorProp = _serializedSettings.FindProperty("aiMessageFontColor");
            
            _userMessageColorProp = _serializedSettings.FindProperty("userMessageColor");
            _userMessageFontColorProp = _serializedSettings.FindProperty("userMessageFontColor");
            
            _backgroundColorProp = _serializedSettings.FindProperty("backgroundsColor");
            _inputFieldFontColorProp = _serializedSettings.FindProperty("inputFieldFontColor");
            _sendButtonProp = _serializedSettings.FindProperty("sendButton");
            
            _faqSampleColorProp = _serializedSettings.FindProperty("faqSampleColor");
            _faqSampleFontColorProp = _serializedSettings.FindProperty("faqSampleFontColor");
            _suggestedQuestionsProp = _serializedSettings.FindProperty("suggestedQuestions");
        }

        /// <summary>
        /// Renders the editor window GUI.
        /// </summary>
        private void OnGUI()
        {
            _targetWidget = (WidgetView)EditorGUILayout.ObjectField("Widget", _targetWidget, typeof(WidgetView), true);

            if (_targetWidget == null)
            {
                EditorGUILayout.HelpBox("Drag and drop your widget from the scene", MessageType.Info);
                return;
            }

            _serializedSettings.Update();

            EditorGUILayout.LabelField("Chat Settings", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(_tenateProp, new GUIContent("Tenate"));
            EditorGUILayout.PropertyField(_endpointProp, new GUIContent("Endpoint"));
            
            EditorGUILayout.PropertyField(_headerLogoProp, new GUIContent("Header logo"));
            EditorGUILayout.PropertyField(_headerColorProp, new GUIContent("Header color"));
            EditorGUILayout.PropertyField(_headerFontColorProp, new GUIContent("Header font color"));
            EditorGUILayout.PropertyField(_headerTextProp, new GUIContent("Header text"));
            
            EditorGUILayout.PropertyField(_aiMessageLogoProp, new GUIContent("AI message logo"));
            EditorGUILayout.PropertyField(_aiMessageColorProp, new GUIContent("AI message background color"));
            EditorGUILayout.PropertyField(_aiMessageFontColorProp, new GUIContent("AI message font color"));
            
            EditorGUILayout.PropertyField(_userMessageColorProp, new GUIContent("User message background color"));
            EditorGUILayout.PropertyField(_userMessageFontColorProp, new GUIContent("User message font color"));
            
            EditorGUILayout.PropertyField(_backgroundColorProp, new GUIContent("Background color"));
            EditorGUILayout.PropertyField(_inputFieldFontColorProp, new GUIContent("Fonts color"));
            EditorGUILayout.PropertyField(_sendButtonProp, new GUIContent("Send button image"));
            
            EditorGUILayout.PropertyField(_faqSampleColorProp, new GUIContent("FAQ color"));
            EditorGUILayout.PropertyField(_faqSampleFontColorProp, new GUIContent("FAQ font color"));
            EditorGUILayout.PropertyField(_suggestedQuestionsProp, new GUIContent("Questions"), true);

            _serializedSettings.ApplyModifiedProperties();

            if (GUILayout.Button("Apply settings"))
            {
                _serializedSettings.ApplyModifiedProperties();
                _targetWidget.ApplySettings();
                EditorUtility.SetDirty(_targetWidget);
            }

            if (GUILayout.Button("Clear FAQ sample field"))
            {
                _targetWidget.ClearSamples();
                EditorUtility.SetDirty(_targetWidget);
            }
        }
    }
}