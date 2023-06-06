using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public Texture2D image;
    public int blocksPerLine = 4;

    private Block emptyBlock;
    
    void Start()
    {
        CreatePuzzle();
    }

    void CreatePuzzle()
    {
        Texture2D[,] imageSlices = ImageSlicer.GetSlices(image, blocksPerLine);

        for (int y = 0; y < blocksPerLine; y++)
        {
            for (int x = 0; x < blocksPerLine; x++)
            {
                GameObject blockObject=GameObject.CreatePrimitive(PrimitiveType.Quad);
                blockObject.transform.position = -Vector2.one * (blocksPerLine - 1) * .5f + new Vector2(x, y);
                blockObject.transform.parent = transform;

                Block block = blockObject.AddComponent<Block>();
                block.OnBlockPressed += PlayerMoveBlockInput;
                block.Init(new Vector2Int(x, y),imageSlices[x,y]);
                
                if (y == 0 && x == blocksPerLine - 1)
                {
                    blockObject.SetActive(false);
                    emptyBlock = block;
                }
            }
        }

        Camera.main.orthographicSize = blocksPerLine * .55f;
    }
    
    void PlayerMoveBlockInput(Block blockToMove)
    {
        if((blockToMove.transform.position - emptyBlock.transform.position).sqrMagnitude == 1)
        {
            Vector2 targetPosition = emptyBlock.transform.position;
            emptyBlock.transform.position = blockToMove.transform.position;
            blockToMove.transform.position = targetPosition;
            
        }
    }
}

