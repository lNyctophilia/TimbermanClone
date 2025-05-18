using UnityEngine;
using UnityEngine.UI;

public class EndingAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _scorePanel;
    [SerializeField] private GameObject _homeButton;

    [Header("Settings")]
    [SerializeField] private float _duration = 0.7f;


    private void Awake() => GameManager.OnGameStateChanged += GameStateHandler;
    private void OnDestroy() => GameManager.OnGameStateChanged -= GameStateHandler;
    private void GameStateHandler(GameState _currentState)
    {
        switch (_currentState)
        {
            case GameState.Ending: Animate(); break;
        }
    }
    private void Animate()
    {
        _scorePanel.transform.localPosition = new Vector3(0, Screen.height + 230);
        _homeButton.transform.localPosition = new Vector3(0, -Screen.height - 980);
        LeanTween.moveLocalY(_scorePanel, 230, _duration);
        LeanTween.moveLocalY(_homeButton, -980, _duration);

    }
}
