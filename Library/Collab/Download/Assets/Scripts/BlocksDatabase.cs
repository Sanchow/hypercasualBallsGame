using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Blocks Database")]
[System.Serializable]
public class BlocksDatabase : ScriptableObject
{
   
    private void OnEnable() {
        if(blocks == null){
            Debug.Log("Creating new dictionary!");
            blocks = new BlockTypePrefabDictionary();
        }
        foreach(BlockType b in BlockType.GetValues(typeof(BlockType))){
            if(!blocks.ContainsKey(b)){
                blocks.Add(b, null);
            }
        }
    }

    [SerializeField]
    public BlockTypePrefabDictionary blocks;
    public GameObject rowPrefab;

    public GameObject GetBlockPrefab(BlockType blockType){
        if(blocks.ContainsKey(blockType)){
            return blocks[blockType];
        }else {
            return null;
        }
    }
}



