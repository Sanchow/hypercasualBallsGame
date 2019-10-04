public enum BlockType{SQUARE, TRIANGLE};

[System.Serializable]
public class BlockData
{
    public int value;
    
    public BlockType type;

    public BlockData(){
        value = 0;
        type = BlockType.SQUARE;
    }

    public bool IsValidBlock{
        get{
            return value > 0;
        }
    }
}
