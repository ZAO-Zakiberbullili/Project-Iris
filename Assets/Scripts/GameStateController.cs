using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// [DefaultExecutionOrder(-100)]
public class GameStateController : MonoBehaviour
{
    private static GameStateController _instance;
    static public GameStateController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameStateController>();
                Debug.Log(_instance);
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
    public enum GameState
    {
        Normal,
        Pause,
        Dialogue,
    }

    private GameState _currentState;

    public GameState CurrentState => _currentState;
    private InputSystem _inputSystem;
    public Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        Instance = this;
        _inputSystem = new InputSystem();
    }

    private void OnEnable()
    {
        _inputSystem.Enable();
    }
    private void OnDisable()
    {
        _inputSystem.Disable();
    }


    public void ChangeGameState(GameState newState)
    {
        _currentState = newState;

        switch (newState)
        {
            case GameState.Normal:
                _inputSystem.Move.Enable();
                _inputSystem.Attack.Enable();

                break;
            case GameState.Dialogue:
                _inputSystem.Move.Disable();
                _inputSystem.Attack.Disable();

                break;
            case GameState.Pause:

                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }
}
