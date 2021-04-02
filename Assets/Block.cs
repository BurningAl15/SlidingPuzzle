using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public event System.Action<Block> OnBlockPressed;

    public Vector2Int coord;

    public void Init(Vector2Int startingCoord, Texture2D image)
    {
        coord = startingCoord;
        GetComponent<MeshRenderer>().material.mainTexture = image;
    }
    
    private void OnMouseDown()
    {
        OnBlockPressed?.Invoke(this);
    }
}
