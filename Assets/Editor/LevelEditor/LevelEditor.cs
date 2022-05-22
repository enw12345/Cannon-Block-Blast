using UnityEditor;
using UnityEngine;

namespace LevelEditor
{
    [CustomEditor(typeof(Level))]
    public class LevelEditor : Editor
    {
        private GUILayoutOption[] blocksDisplayOptions;
        private SerializedProperty blocksProperty;
        private SerializedProperty columnProperty;
        private SerializedProperty nextBlockProperty;
        private SerializedProperty objectivesAmount;
        private SerializedProperty objectivesProperty;
        private SerializedProperty rowProperty;

        private const float Spacing = 30f;

        private void OnEnable()
        {
            rowProperty = serializedObject.FindProperty("rows");
            columnProperty = serializedObject.FindProperty("columns");
            objectivesProperty = serializedObject.FindProperty("objectives");
            objectivesAmount = serializedObject.FindProperty("objectiveAmountsToComplete");
            blocksProperty = serializedObject.FindProperty("Blocks");
            nextBlockProperty = serializedObject.FindProperty("newBlockToSpawn");
        }

        public override void OnInspectorGUI()
        {
            EditorGUIUtility.labelWidth = 50f;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(rowProperty);
            EditorGUILayout.PropertyField(columnProperty);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(Spacing);
            EditorGUIUtility.labelWidth = 170f;
            EditorGUILayout.PropertyField(objectivesProperty);

            EditorGUILayout.PropertyField(nextBlockProperty);

            #region Objective Amount Editor
            EditorGUILayout.Space(10f);

            EditorGUILayout.LabelField("Objective Editor");
           
            objectivesAmount.arraySize = objectivesProperty.arraySize;
            for (var i = 0; i < objectivesProperty.arraySize; i++)
            {
                EditorGUILayout.BeginVertical();
                EditorGUILayout.PropertyField(objectivesAmount.GetArrayElementAtIndex(i), new GUIContent("Objective " + i + " Amount"));
                EditorGUILayout.EndVertical();
            }

            #endregion

            EditorGUILayout.Space(Spacing);

            #region Level Grid

            EditorGUILayout.LabelField("Level Grid:");
            EditorGUILayout.Space(10f);
            for (var i = 0; i < rowProperty.intValue; i++)
            {
                EditorGUILayout.BeginHorizontal();

                for (var j = 0; j < columnProperty.intValue; j++)
                {
                    var index = i * rowProperty.intValue + j;

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
}