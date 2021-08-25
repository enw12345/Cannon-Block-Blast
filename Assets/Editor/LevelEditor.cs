using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    SerializedProperty rowProperty;
    SerializedProperty columnProperty;
    SerializedProperty objectivesProperty;
    SerializedProperty objectivesAmount;
    SerializedProperty blocksProperty;

    GUILayoutOption[] blocksDisplayOptions;

    float spacing = 30f;
    private void OnEnable()
    {
        rowProperty = serializedObject.FindProperty("rows");
        columnProperty = serializedObject.FindProperty("columns");
        objectivesProperty = serializedObject.FindProperty("objectives");
        objectivesAmount = serializedObject.FindProperty("objectiveAmounts");
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

        EditorGUILayout.Space(spacing);
        EditorGUIUtility.labelWidth = 170f;
        EditorGUILayout.PropertyField(objectivesProperty);

        #region Objective Amount Editor
        EditorGUILayout.LabelField("Objective Editor");
        EditorGUILayout.Space(10f);

        for (int i = 0; i < objectivesProperty.arraySize; i++)
        {
            objectivesAmount.InsertArrayElementAtIndex(i);
            SerializedProperty objectiveAmount = objectivesAmount.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(objectiveAmount);
            EditorGUILayout.EndVertical();

        }
        #endregion

        EditorGUILayout.Space(spacing);

        #region Level Grid
        EditorGUILayout.LabelField("Level Grid:");
        EditorGUILayout.Space(10f);
        int index = 0;
        for (int i = 0; i < rowProperty.intValue; i++)
        {
            EditorGUILayout.BeginHorizontal();

            for (int j = 0; j < columnProperty.intValue; j++)
            {
                index = i * rowProperty.intValue + j;

                if (blocksProperty.GetArrayElementAtIndex(index) == null)
                    blocksProperty.InsertArrayElementAtIndex(index);

                EditorGUILayout.PropertyField(blocksProperty.GetArrayElementAtIndex(index), GUIContent.none);
            }

            EditorGUILayout.EndHorizontal();
        }
        #endregion

        serializedObject.ApplyModifiedProperties();
    }
}