using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{

    public List<Transform> spawnPoints;

    public bool IsEmpty { 
        get{
            Block[] blocks = GetComponentsInChildren<Block>();
            return blocks.Length == 0;
        }
    }

}
