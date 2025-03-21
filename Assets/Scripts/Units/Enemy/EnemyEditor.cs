using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace Units.Enemy
{
    
    public class EnemyEditor : Editor
    {
        // SerializedProperty m_IntProp;
        // SerializedProperty m_VectorProp;
        // SerializedProperty m_GameObjectProp;
        // SerializedProperty m_IdProp;
        //
        // void OnEnable()
        // {
        //     // Fetch the objects from the GameObject script to display in the inspector
        //     m_IntProp = serializedObject.FindProperty("integ");
        //     m_VectorProp = serializedObject.FindProperty("Vector");
        //     m_GameObjectProp = serializedObject.FindProperty("MyGameObject");
        //     m_IdProp = serializedObject.FindProperty("test");
        // }
        //
        // public override void OnInspectorGUI()
        // {
        //     //The variables and GameObject from the MyGameObject script are displayed in the Inspector with appropriate labels
        //     EditorGUILayout.PropertyField(m_IntProp, new GUIContent("Int Field"), GUILayout.Height(20));
        //     EditorGUILayout.PropertyField(m_VectorProp, new GUIContent("Vector Object"));
        //     EditorGUILayout.PropertyField(m_GameObjectProp, new GUIContent("Game Object"));
        //     EditorGUILayout.PropertyField(m_IdProp, new GUIContent("test"));
        //
        //     // Apply changes to the serializedProperty - always do this at the end of OnInspectorGUI.
        //     serializedObject.ApplyModifiedProperties();
        // }
    }
}