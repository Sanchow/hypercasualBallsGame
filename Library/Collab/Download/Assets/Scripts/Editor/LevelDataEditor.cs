using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelData))]
public class LevelDataEditor : Editor {

    LevelData levelData;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        EditorGUI.BeginChangeCheck();

        Undo.RecordObject(target, "Updated level layout");

        #region Level Options

        EditorGUILayout.BeginVertical("Box", GUILayout.Width(435f));
        EditorGUILayout.LabelField("Level Options");

            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Starting Row", GUILayout.ExpandWidth(true));
                levelData.startingRow = EditorGUILayout.IntField(levelData.startingRow,GUILayout.Width(40f));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Number of balls",GUILayout.ExpandWidth(true));
                levelData.numberOfBalls = EditorGUILayout.IntField(levelData.numberOfBalls, GUILayout.Width(40f));
            EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();

        #endregion
        

        #region Level layout editing
        EditorGUILayout.BeginVertical("Box",GUILayout.Width(400f));
            EditorGUILayout.LabelField("Level Layout",GUILayout.ExpandWidth(false));
            EditorGUILayout.BeginVertical("Box",GUILayout.ExpandWidth(false));

                EditorGUILayout.BeginHorizontal();

                    if(GUILayout.Button("Add row")){
                        Undo.RecordObject(target, "Added row");
                        levelData.AddRow();
                    }
                    if(GUILayout.Button("Clear Layout")){
                        Undo.RecordObject(target, "Cleared level layout");
                        levelData.ClearData();
                    }

                EditorGUILayout.EndHorizontal();

                    for (int i = levelData.RowCount - 1; i >= 0; i--)
                    {
                        EditorGUILayout.BeginHorizontal("Box",GUILayout.ExpandWidth(false));
                            EditorGUILayout.LabelField("Row " + (i + 1), GUILayout.Width(60f));

                            for (int j = 0; j < 9; j++)
                            {
                                EditorGUILayout.BeginVertical("Box");
                                    Undo.RecordObject(target, "Changed block");
                                    
                                    int newValue = EditorGUILayout.IntField(levelData[i][j].value, GUILayout.Width(20f));
                                    levelData[i][j].value = newValue;
                                    levelData[i][j].type = (BlockType)EditorGUILayout.EnumPopup(levelData[i][j].type, GUILayout.Width(20f));
                                
                                EditorGUILayout.EndVertical();
                            }
                            if(GUILayout.Button("Remove",GUILayout.ExpandWidth(false))){
                                levelData.RemoveRow(i);
                            }

                        GUILayout.EndHorizontal();
                }
            GUILayout.EndVertical();

        GUILayout.EndVertical();
        #endregion

        if(EditorGUI.EndChangeCheck()){
            AssetDatabase.SaveAssets();
        }
        
    }

    private void OnEnable() {
        levelData = (LevelData)target;
    }
}
