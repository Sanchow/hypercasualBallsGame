using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Level Data", fileName = "New Level Data")]
public class LevelData : ScriptableObject
{
    
    [SerializeField, HideInInspector]
    private List<BlockData> data;

    [HideInInspector]
    public int startingRow;
    [HideInInspector]
    public int numberOfBalls;

    public GameObject rowPrefab { get{ return blocksDatabase.rowPrefab;}}

    public BlocksDatabase blocksDatabase;

    public int RowCount{
        get{
            return data.Count / Width;
        }
    }

    public int Count{
        get{
            return data.Count;
        }
    }

    public int Width{
        get{
            return 9;
        }
    }

    public List<BlockData> this[int i]{
        get{
            List<BlockData> aux = new List<BlockData>();
            for (int j = 0; j < Width; j++)
            {
                aux.Add(data[j + (Width * i)]);
            }
            return aux;
        }
    }

    public BlockData this[int i, int j]{
        get{
            return data[i * Width + j];
        }
    }

    private void OnEnable(){
        if(data == null){
            ClearData();
        }
    }

    
    public void ClearData(){
        data = new List<BlockData>();
        for (int i = 0; i < Width; i++)
        {
            data.Add(new BlockData());
        }
    }

    public void AddRow(){
        for (int i = 0; i < Width; i++)
        {
            data.Add(new BlockData());
        }
    }

    public void RemoveRow(int index){
        data.RemoveRange(index * Width, Width);
    }

    public GameObject GetBlockPrefab(BlockType type){
        return blocksDatabase.GetBlockPrefab(type);
    }


}