using System.Collections;
using UnityEngine;

public class PlayerDieAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _tombstone;
    [SerializeField] private ParticleSystem _particle;


    [Header("Settings")]
    [SerializeField] private float _duration = 0.5f;


    private void Awake()
    {
        GameManager.OnGameStateChanged += GameStateHandler;
        Transition.OnFadeTransitionFinished += DeactiveTombstone;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateHandler;
        Transition.OnFadeTransitionFinished -= DeactiveTombstone;
    }


    private void GameStateHandler(GameState _currentState)
    {
        if (_currentState == GameState.Ending)
            DieAnim();
    }
    private void DieAnim()
    {
        LeanTween.alpha(_player, 0, _duration).setOnComplete(() => { _player.SetActive(false); });

        _tombstone.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + 2);
        _tombstone.SetActive(true);

        LeanTween.moveLocalY(_tombstone, _player.transform.position.y, 0.25f).setEaseInQuad().setOnComplete(() => { _particle.Play(); });
    }
    private void DeactiveTombstone()
    {
        _tombstone.SetActive(false);
        _player.GetComponent<SpriteRenderer>().color = Color.white;
        _player.SetActive(true);
    }
}
