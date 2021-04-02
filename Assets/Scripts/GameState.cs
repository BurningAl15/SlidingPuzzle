using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    Waiting,
    Start,
    Playing,
    Pause,
    End
}

public class GameState : MonoBehaviour
{
    public static GameState _instance;
    
    public GameStates currentGameState;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        
        GameState_Waiting();
    }

    public bool IsWaiting()
    {
        return currentGameState == GameStates.Waiting;
    }
    
    public bool IsStarting()
    {
        return currentGameState == GameStates.Start;
    }
    
    public bool IsPlaying()
    {
        return currentGameState == GameStates.Playing;
    }
    
    public bool IsPaused()
    {
        return currentGameState == GameStates.Pause;
    }
    
    public bool IsEnding()
    {
        return currentGameState == GameStates.End;
    }

    public void GameState_Waiting()
    {
        currentGameState = GameStates.Waiting;
    }
    
    public void GameState_Starting()
    {
        currentGameState = GameStates.Start;
    }
    
    public void GameState_Playing()
    {
        currentGameState = GameStates.Playing;
    }
    
    public void GameState_Pause()
    {
        currentGameState = GameStates.Pause;
    }
    
    public void GameState_End()
    {
        currentGameState = GameStates.End;
    }
}
