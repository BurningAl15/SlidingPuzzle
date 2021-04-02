using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PuzzleManager puzzleManager;
    private Coroutine currentCoroutine = null;
    
    private void Start()
    {
        if (currentCoroutine == null)
            currentCoroutine = StartCoroutine(InitializeGame());
    }

    IEnumerator InitializeGame()
    {
        GameState._instance.GameState_Starting();
        yield return puzzleManager.Initialize();
        yield return TransitionManager._instance.TransitionEffect_FadeOut();
        yield return new WaitForSeconds(2f);
        GameState._instance.GameState_Playing();
        currentCoroutine = null;
    }
}
