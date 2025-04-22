using Chatcloud.CodeBase.ScriptableObjects;
using Chatcloud.CodeBase.UI;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Chatcloud.CodeBase.Editor
{
    public class WidgetSettingEditor : EditorWindow
    {
        private WidgetView _targetWidget;
        
        private WidgetSettings _settings;

        private SerializedObject _serializedSettings;
        private SerializedProperty _headerLogoProp;
        private SerializedProperty _headerColorProp;
        private SerializedProperty _headerFontColorProp;
        private SerializedProperty _headerTextProp;
        
        private SerializedProperty _messageLogoProp;
        private SerializedProperty _messageColorProp;
        
        private SerializedProperty _requestSampleColorProp;
        private SerializedProperty _suggestedQuestionsProp;
        
        private SerializedProperty _backgroundColorProp;
        private SerializedProperty _fontColorProp;
        private SerializedProperty _sendButtonProp;


        [MenuItem("AI Widget/Settings Editor")]
        public static void ShowWindow()
        {
            GetWindow<WidgetSettingEditor>("AI Widget Settings");
        }

        private void OnEnable()
        {
            _settings = AssetDatabase.LoadAssetAtPath<WidgetSettings>("Assets/Resources/WidgetSettings.asset");
            if (_settings == null)
            {
                _settings = CreateInstance<WidgetSettings>();
                AssetDatabase.CreateAsset(_settings, "Assets/WidgetSettings.asset");
                AssetDatabase.SaveAssets();
            }

            _serializedSettings = new SerializedObject(_settings);
            
            _headerLogoProp = _serializedSettings.FindProperty("headerLogo");
            _headerColorProp = _serializedSettings.FindProperty("headerColor");
            _headerFontColorProp = _serializedSettings.FindProperty("headerFontColor");
            _headerTextProp = _serializedSettings.FindProperty("headerText");
            
            _messageLogoProp = _serializedSettings.FindProperty("messageLogo");
            _messageColorProp = _serializedSettings.FindProperty("messageColor");
            
            _backgroundColorProp = _serializedSettings.FindProperty("backgroundsColor");
            _fontColorProp = _serializedSettings.FindProperty("fontColor");
            _sendButtonProp = _serializedSettings.FindProperty("sendButton");
            
            _requestSampleColorProp = _serializedSettings.FindProperty("requestSampleColor");
            _suggestedQuestionsProp = _serializedSettings.FindProperty("suggestedQuestions");
        }

        private void OnGUI()
        {
            _targetWidget = (WidgetView)EditorGUILayout.ObjectField("Widget", _targetWidget, typeof(WidgetView), true);
            
            if (_targetWidget == null)
            {
                EditorGUILayout.HelpBox("Drag and drop your widget from the scene", MessageType.Info);
                return;
            }
            
            _serializedSettings.Update();

            EditorGUILayout.LabelField("AI Widget Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_headerLogoProp, new GUIContent("Header logo"));
            EditorGUILayout.PropertyField(_headerColorProp, new GUIContent("Header color"));
            EditorGUILayout.PropertyField(_headerFontColorProp, new GUIContent("Header font color"));
            EditorGUILayout.PropertyField(_headerTextProp, new GUIContent("Header text"));
            
            EditorGUILayout.PropertyField(_messageLogoProp, new GUIContent("Message logo"));
            EditorGUILayout.PropertyField(_messageColorProp, new GUIContent("Message color"));
            
            EditorGUILayout.PropertyField(_backgroundColorProp, new GUIContent("Background color"));
            EditorGUILayout.PropertyField(_fontColorProp, new GUIContent("Fonts color"));
            EditorGUILayout.PropertyField(_sendButtonProp, new GUIContent("Send button image"));
            
            EditorGUILayout.PropertyField(_requestSampleColorProp, new GUIContent("Request sample color"));
            
            EditorGUILayout.PropertyField(_suggestedQuestionsProp, new GUIContent("Questions"), true);

            _serializedSettings.ApplyModifiedProperties();
            
            if (GUILayout.Button("Apply settings"))
            {
                _serializedSettings.ApplyModifiedProperties();
                _targetWidget.ApplySettings();
                EditorUtility.SetDirty(_targetWidget);
            }

            if (GUILayout.Button("Clear request sample field"))
            {
                _targetWidget.ClearSamples();
                EditorUtility.SetDirty(_targetWidget);
            }
        }
    }
}