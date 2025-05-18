using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("System")]
    public static GameManager Instance;
    public static event Action<GameState> OnGameStateChanged;


    // ----- Variables -----
    public GameState _currentState {get; private set;}
    private bool _readyToMove = true;
    public bool ReadyToMove => _readyToMove;


    private void Awake()
    {
        Instance = this;
        _currentState = GameState.Menu;
    }


    public void ChangeState(GameState _newState)
    {
        if(_currentState == _newState) return;

        _currentState = _newState;
        OnGameStateChanged?.Invoke(_currentState);
        Debug.Log($"Current State: {_currentState}");
    }
    public void SetState(int _stateIndex) => ChangeState((GameState)_stateIndex);


    #region ----- TESTING -----

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeState(GameState.Menu);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeState(GameState.Playing);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeState(GameState.Ending);
        }
    }

    #endregion
}

public enum GameState
{
    Menu,
    Playing,
    Ending
}
