using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderParent : MonoBehaviour
{
    [SerializeField] Collider2D collider;

    public void TurnOffCollider()
    {
        collider.enabled = false;
    }
   
    public void TurnOnCollider()
    {
        collider.enabled = true;
    }
}
