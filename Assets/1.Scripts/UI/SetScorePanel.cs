using UnityEngine;
using UnityEngine.UI;

public class SetScorePanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _highScoreText;

    private void Awake() => GameManager.OnGameStateChanged += GameStateHandler;
    private void OnDestroy() => GameManager.OnGameStateChanged -= GameStateHandler;
    private void GameStateHandler(GameState _currentState)
    {
        if (_currentState != GameState.Ending) return;

        _scoreText.text = ScoreManager.Instance.CurrentScore.ToString();
        _highScoreText.text = ScoreManager.Instance.HighScore.ToString();
    }
}
