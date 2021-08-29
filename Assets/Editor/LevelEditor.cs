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
    SerializedProperty nextBlockProperty;

    GUILayoutOption[] blocksDisplayOptions;

    float spacing = 30f;
    private void OnEnable()
    {
        rowProperty = serializedObject.FindProperty("rows");
        columnProperty = serializedObject.FindProperty("columns");
        objectivesProperty = serializedObject.FindProperty("objectives");
        objectivesAmount = serializedObject.FindProperty("objectiveAmounts");
        blocksProperty = serializedObject.FindProperty("Blocks");
        nextBlockProperty = serializedObject.FindProperty("newBlockToSpawn");
    }

    public override void OnInspectorGUI()
    {
        // DrawDefaultInspector();
        EditorGUIUtility.labelWidth = 50f;
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(rowProperty);
        EditorGUILayout.PropertyField(columnProperty);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(spacing);
        EditorGUIUtility.labelWidth = 170f;
        EditorGUILayout.PropertyField(objectivesProperty);

        EditorGUILayout.PropertyField(nextBlockProperty);

        #region Objective Amount Editor
        EditorGUILayout.LabelField("Objective Editor");
        EditorGUILayout.Space(10f);

        for (int i = 0; i < objectivesProperty.arraySize; i++)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(objectivesAmount.GetArrayElementAtIndex(i));
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