using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // TimeScale olayları ve Kuşu aktif etme olayları var unutma.

    [Header("System")]
    public static UIManager Instance;


    [Header("References")]
    [SerializeField] private GameObject[] _canvas;


    private void Awake()
    {
        Instance = this;
        OpenCanvas(0);

        GameManager.OnGameStateChanged += ChangeCanvas;
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= ChangeCanvas;
    }


    private void ChangeCanvas(GameState _currentState)
    {
        StartCoroutine(OpenCanvas((int)_currentState));
    }
    private IEnumerator OpenCanvas(int _canvasIndex)
    {
        yield return new WaitForSeconds(Transition.Instance.FadeDuration);

        LeanTween.cancel(_canvas[_canvasIndex]);

        _canvas[_canvasIndex].gameObject.SetActive(true);
        _canvas[_canvasIndex].GetComponent<CanvasGroup>().alpha = 0;
        
        LeanTween.alphaCanvas(_canvas[_canvasIndex].GetComponent<CanvasGroup>(), 1, Transition.Instance.FadeDuration).setIgnoreTimeScale(true);;

        for(int i = 0; i < _canvas.Length; i++)
        {
            if(i != _canvasIndex)
            {
                int currentIndex = i;

                LeanTween.alphaCanvas(_canvas[currentIndex].GetComponent<CanvasGroup>(), 0, Transition.Instance.FadeDuration / 2f).setOnComplete(() => { 
                    _canvas[currentIndex].SetActive(false); 
                }).setIgnoreTimeScale(true);
            }
        }
    }
}