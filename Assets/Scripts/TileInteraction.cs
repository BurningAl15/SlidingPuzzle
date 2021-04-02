using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    None,Up,Down,Left,Right
}

public class TileInteraction : ColliderParent
{
    [Header("Gameplay Id's")]
    [SerializeField] private int index;
    public int Index { get; }
    [SerializeField] private int currentPosition;

    public int CurrentPosition
    {
        get => currentPosition;
        set => currentPosition = value;
    }

    [Header("Overlap Circle Data")]
    private Transform tilePosition;
    [SerializeField] private LayerMask _layerMask;
    private bool isMoving = false;

    [SerializeField] private SpriteRenderer tileSprite;
    [SerializeField] private MeshRenderer meshRenderer;
    private Coroutine currentCoroutine = null;
    public event Action OnCheckAnswer;

    private void Awake()
    {
        tilePosition = transform;
    }

    public void InitSprite(Sprite _sprite)
    {
        if (tileSprite != null)
            tileSprite.sprite = _sprite;
        else
            print("Don't Have Sprite Renderer");
    }

    public void InitTexture(Texture2D _texture)
    {
        if (meshRenderer != null)
            meshRenderer.material.mainTexture = _texture;
        else
            print("Don't Have MeshRenderer");
    }
    
    private void OnMouseDown()
    {
        if (GameState._instance.IsPlaying())
        {
            if (currentCoroutine == null)
            {
                Vector3 temp = CheckEmptySlot();
                if (temp != tilePosition.position)
                    currentCoroutine = StartCoroutine(Move(CheckEmptySlot()));
            }
        }
    }

    Vector3 CheckEmptySlot()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(tilePosition.position, TileUtils.collisionRadius, _layerMask);
        print(colliders.Length);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (!colliders[i].GetComponent<PositionSetter>().isFull)
                return colliders[i].transform.position;
        }

        print("Can't move");
        return tilePosition.position;
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        yield return Scale(false);
        while ((targetPos- tilePosition.position).sqrMagnitude>Mathf.Epsilon)
        {
            tilePosition.position = Vector3.MoveTowards(tilePosition.position, targetPos, TileUtils.moveSpeed * Time.deltaTime);
            yield return null;
        }

        tilePosition.position = targetPos;
        yield return Scale(true);
        isMoving = false;
        OnCheckAnswer?.Invoke();

        currentCoroutine = null;
    }

    IEnumerator Scale(bool MaxToMin)
    {
        float scale = MaxToMin ? TileUtils.scaleMax : TileUtils.scaleMin;
        tilePosition.localScale = Vector3.one * scale;

        float timeStep = Time.deltaTime * TileUtils.timeStep;
        
        if (MaxToMin)
        {
            while (scale > TileUtils.scaleMin)
            {
                tilePosition.localScale = Vector3.one * scale;
                scale -= timeStep;
                yield return null;
            }

            scale = TileUtils.scaleMin;
        }
        else
        {
            while (scale < TileUtils.scaleMax)
            {
                tilePosition.localScale = Vector3.one * scale;
                scale += timeStep;
                yield return null;
            }

            scale = TileUtils.scaleMax;
        }

        tilePosition.localScale = Vector3.one * scale;

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PositionSlot"))
        {
            PositionSetter temp = other.GetComponent<PositionSetter>();
            currentPosition = temp.index;
            temp.isFull = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PositionSlot"))
        {
            PositionSetter temp = other.GetComponent<PositionSetter>();
            temp.isFull = false;
        }            
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0, 0);
        Gizmos.DrawWireSphere(this.transform.position,TileUtils.collisionRadius);
    }
}
