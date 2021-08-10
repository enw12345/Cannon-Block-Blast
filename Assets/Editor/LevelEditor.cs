using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    SerializedProperty rowProperty;
    SerializedProperty columnProperty;
    SerializedProperty objectivesProperty;
    SerializedProperty blocksProperty;
    SerializedProperty blockProperty;

    GUILayoutOption[] blocksDisplayOptions;

    float spacing = 30f;
    private void OnEnable()
    {
        rowProperty = serializedObject.FindProperty("rows");
        columnProperty = serializedObject.FindProperty("columns");
        objectivesProperty = serializedObject.FindProperty("objectives");
        blocksProperty = serializedObject.FindProperty("Blocks");
    }

    public override void OnInspectorGUI()
    {
        EditorGUIUtility.labelWidth = 50f;
        // DrawDefaultInspector();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(rowProperty);
        EditorGUILayout.PropertyField(columnProperty);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.PropertyField(objectivesProperty);

        EditorGUILayout.Space(spacing);

        EditorGUILayout.LabelField("Level Grid:");
        EditorGUILayout.Space(10f);

        int index = 0;
        for (int i = 0; i < rowProperty.intValue; i++)
        {
            EditorGUILayout.BeginHorizontal();

            for (int j = 0; j < columnProperty.intValue; j++)
            {
                index = i * rowProperty.intValue + j;

                blockProperty = blocksProperty.GetArrayElementAtIndex(index);

                EditorGUILayout.PropertyField(blockProperty, GUIContent.none);
            }

            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }


}