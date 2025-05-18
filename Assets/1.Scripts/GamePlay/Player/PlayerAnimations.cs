using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();

        PlayerHitting.OnPlayerHitting += () => { if(GameManager.Instance._currentState != GameState.Ending) _anim.Play("Hitting"); };
    }
    private void OnDestroy() => PlayerHitting.OnPlayerHitting -= () => { if(GameManager.Instance._currentState != GameState.Ending) _anim.Play("Hitting"); };
}
