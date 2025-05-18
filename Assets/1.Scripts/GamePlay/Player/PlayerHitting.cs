using System;
using UnityEngine;

public class PlayerHitting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform[] _playerPos;
    private SpriteRenderer _playerSpriteRenderer;
    

    // ----- SYSTEM -----
    public static event Action OnPlayerHitting;

    private void Awake()
    {
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Hitting(string _func)
    {
        if (GameManager.Instance._currentState == GameState.Ending) return;
        
        switch (_func)
        {
            case "Left":
                _playerSpriteRenderer.flipX = false;
                transform.position = _playerPos[0].position;
                break;

            case "Right":
                _playerSpriteRenderer.flipX = true;
                transform.position = _playerPos[1].position;
                break;
        }
        OnPlayerHitting?.Invoke();
    }
}
