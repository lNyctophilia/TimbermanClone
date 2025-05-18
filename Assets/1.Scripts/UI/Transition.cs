using System;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _blackImage;
    

    [Header("Settings")]
    [SerializeField] private float _fadeDuration = 0.3f;
    public float FadeDuration => _fadeDuration;


    [Header("System")]
    public static Transition Instance;
    public static event Action OnFadeTransition;
    public static event Action OnFadeTransitionFinished;


    private void Awake()
    {
        Instance = this;
        _blackImage.gameObject.SetActive(false);

        GameManager.OnGameStateChanged += Fade;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= Fade;
    }


    public void Fade(GameState _currentState)
    {
        if(_currentState != GameState.Menu) return;

        _blackImage.gameObject.SetActive(true);
        _blackImage.color = new Color(0, 0, 0, 0);
        LeanTween.cancel(_blackImage.gameObject);
        LeanTween.alpha(_blackImage.rectTransform, 1, _fadeDuration).setOnComplete(() => {
            LeanTween.alpha(_blackImage.rectTransform, 0, _fadeDuration).setOnComplete(() => { _blackImage.gameObject.SetActive(false); }).setDelay(0.1f);
            OnFadeTransitionFinished?.Invoke();
        });

        OnFadeTransition?.Invoke();
    }
}
