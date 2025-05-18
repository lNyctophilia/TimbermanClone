using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("System")]
    public static ScoreManager Instance;
    public static event Action OnScoreIncreased;


    [Header("Variables")]
    [SerializeField] private int _currentScore;
    [SerializeField] private int _highScore;
    [SerializeField] private string _highScorePrefsKey = "Highscore";

    public int CurrentScore => _currentScore;
    public int HighScore => _highScore;


    [Header("References")]
    public Text _scoreText;


    private void Awake()
    {
        Instance = this;
        _highScore = PlayerPrefs.GetInt(_highScorePrefsKey, 0);

        PlayerHitting.OnPlayerHitting += IncreaseScore;
        GameManager.OnGameStateChanged += ScoreReset;
    }
    private void OnDestroy()
    {
        PlayerHitting.OnPlayerHitting -= IncreaseScore;
        GameManager.OnGameStateChanged -= ScoreReset;
    }


    public void ScoreReset(GameState _currentState)
    {
        if(_currentState != GameState.Menu) return;

        _currentScore = 0;
        _scoreText.text = _currentScore.ToString();
    }
    public void IncreaseScore()
    {
        _currentScore++;
        _scoreText.text = _currentScore.ToString();

        if (_currentScore > PlayerPrefs.GetInt(_highScorePrefsKey, 0))
        {
            PlayerPrefs.SetInt(_highScorePrefsKey, _currentScore);
            _highScore = _currentScore;
        }

        OnScoreIncreased?.Invoke();
    }
    private void OnApplicationQuit() => PlayerPrefs.Save();
}
