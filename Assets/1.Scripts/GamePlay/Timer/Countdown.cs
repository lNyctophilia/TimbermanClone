using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _timerImage;
    [SerializeField] private Image _flashImage;

    [Header("Settings")]
    [SerializeField] private float _maxTime = 10f;
    [SerializeField] private float _increaseAmount = 0.25f;
    private float _remainingTime;


    private void Awake() => SubscribeEvents();
    private void OnDestroy() => UnsubscribeEvents();

    private void Update()
    {
        if(GameManager.Instance._currentState != GameState.Playing) return;

        if (_remainingTime > 0)
        {
            _remainingTime -= Time.deltaTime;
            _timerImage.fillAmount = _remainingTime / _maxTime;
        }
        else
        {
            _remainingTime = 0f;
            _timerImage.fillAmount = 0f;
            GameManager.Instance.ChangeState(GameState.Ending);
        }
    }
    private void ResetTimer(GameState _currentState)
    {
        if(_currentState != GameState.Playing) return;

        _remainingTime = _maxTime;
        _timerImage.fillAmount = 1f;
    }
    private void IncreaseRemainingTime()
    {
        _remainingTime += _increaseAmount;

        if (_remainingTime >= _maxTime)
            _remainingTime = _maxTime;

        StartCoroutine(IncreaseAnimation());
    }
    private IEnumerator IncreaseAnimation()
    {
        _flashImage.fillAmount = _timerImage.fillAmount;
        _timerImage.gameObject.SetActive(false);
        _flashImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.075f);
        _timerImage.gameObject.SetActive(true);
        _flashImage.gameObject.SetActive(false);
    }
    private void SubscribeEvents()
    {
        GameManager.OnGameStateChanged += ResetTimer;
        PlayerHitting.OnPlayerHitting += IncreaseRemainingTime;

    }
    private void UnsubscribeEvents()
    {
        GameManager.OnGameStateChanged -= ResetTimer;
        PlayerHitting.OnPlayerHitting -= IncreaseRemainingTime;
    }
}
