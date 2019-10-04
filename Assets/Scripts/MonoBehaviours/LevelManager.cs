using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelState{WAITING, PLAYING, ACCELERATED, PAUSED, FINISHED, LOST}

[System.Serializable]
public class LevelManager : MonoBehaviour
{
    public LevelState CurrentLevelState{ get; private set;}
    public Events.LevelStateEvent OnStateChanged;
    public Transform levelContainer;
    public Transform spawnPoint;

    public LevelData levelData;
    private GameObject rowPrefab;

    private List<Block> instantiatedBlocks;
    private int rowCounter = 0;

    public float LevelProgress{ 
        get{
            int totalBlocksHP = 0;
            for (int i = 0; i < levelData.RowCount; i++)
            {
                for (int j = 0; j < levelData.Width; j++)
                {
                    totalBlocksHP += levelData[i][j].value;
                }
            }
            
            int currentBlocksHP = 0;
            foreach(Block b in instantiatedBlocks){
                currentBlocksHP += b.currentHealth;
            }

            for (int i = rowCounter; i < levelData.RowCount; i++)
            {
                for (int j = 0; j < levelData.Width; j++)
                {
                    currentBlocksHP += levelData[i][j].value;
                }
            }

            return 1.0f - (float)currentBlocksHP/totalBlocksHP;
        }
    }


    void Start(){
        instantiatedBlocks = new List<Block>();
        rowPrefab = levelData.rowPrefab;

        for (int i = 0; i < levelData.startingRow; i++)
        {
            MoveLevelDown();
            SpawnNextRow();
        }
    }

    void SpawnNextRow(){
        Row rowInstance = Instantiate(rowPrefab, spawnPoint.position, Quaternion.identity, levelContainer).GetComponent<Row>();
        if(rowCounter < levelData.RowCount){
            for (int i = 0; i < levelData.Width; i++)
            {
                if(levelData[rowCounter][i].IsValidBlock){
                    GameObject blockPrefab = levelData.GetBlockPrefab(levelData[rowCounter][i].type);
                    Block blockInstance = Instantiate(blockPrefab, rowInstance.spawnPoints[i]).GetComponent<Block>();
                    blockInstance.Initialize(levelData[rowCounter][i].value);

                    instantiatedBlocks.Add(blockInstance);

                    blockInstance.OnBlockDestroyed += () => RemoveBlockFromList(blockInstance);
                    blockInstance.OnBlockDestroyed += CheckIfAnyBlocksAreLeft;
                }
            }
        }
        rowCounter++;
    }

    void MoveLevelDown(){
        levelContainer.position = new Vector3(levelContainer.position.x, levelContainer.position.y - 0.6f, levelContainer.position.z);
    }

    public void RemoveBlockFromList(Block block){
        instantiatedBlocks.Remove(block);
    }

    public void CheckIfAnyBlocksAreLeft(){
        if(instantiatedBlocks.Count == 0 && rowCounter >= levelData.RowCount){
            ChangeState(LevelState.FINISHED);
        }
    }

    public void ChangeState(LevelState newState){
        if(CurrentLevelState != newState){
            if(HandleLevelStateChanged(CurrentLevelState, newState)){
                if(OnStateChanged != null){
                    OnStateChanged.Invoke(CurrentLevelState, newState); 
                }
                CurrentLevelState = newState; 
            }
        }
    }

    bool HandleLevelStateChanged(LevelState oldState, LevelState newState){
        if(oldState == LevelState.FINISHED || oldState == LevelState.LOST){
            return false;
        }
        if(oldState == LevelState.PLAYING && newState == LevelState.WAITING){
            MoveLevelDown();
            if(rowCounter < levelData.Count){
                SpawnNextRow();
            }
        }
        if(newState == LevelState.ACCELERATED){
            Time.timeScale = 2;
        }
        if(oldState == LevelState.ACCELERATED && newState == LevelState.WAITING){
            Time.timeScale = 1;
        }
        return true;
    }


}
