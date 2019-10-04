using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BlocksDatabase))]
public class BlocksDatabaseEditor : Editor
{
    BlocksDatabase blocksDatabase;
    BlockType aux = BlockType.SQUARE;

    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        GUILayout.BeginHorizontal();
            aux = (BlockType)EditorGUILayout.EnumPopup(aux);
            GameObject aux2 = (GameObject)EditorGUILayout.ObjectField(blocksDatabase.GetBlockPrefab(aux) == null ? null : blocksDatabase.GetBlockPrefab(aux),typeof(GameObject),false);
            if(aux2 != null){
                blocksDatabase.blocks[aux] = aux2;
            }
        GUILayout.EndHorizontal();

    }

    private void OnEnable() {
        blocksDatabase = (BlocksDatabase)target;
    }
}
