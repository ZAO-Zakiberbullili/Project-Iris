using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController
{
    private static GameStateController _instance;
    public static GameStateController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameStateController();
            }
            return _instance;
        }
    }
    
    private GameState _currentState;

    public GameState CurrentState => _currentState;
    public Action<GameState> OnGameStateChanged;

    public void ChangeGameState(GameState newState)
    {
        _currentState = newState;

        switch (newState)
        {
            case GameState.Normal:
                break;
            case GameState.Dialogue:
                break;
            case GameState.Pause:
                break;
            case GameState.Battle:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }
}

public enum GameState
{
    Normal,
    Pause,
    Dialogue,
    Battle,
}
