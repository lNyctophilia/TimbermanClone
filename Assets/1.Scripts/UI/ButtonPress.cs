using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Settings")]
    [SerializeField] private float _pressedScaleRate = 0.85f;
    [SerializeField] private float _duration = 0.05f;
    [SerializeField] private bool isMuted;
    private Vector3 _initialScale;


    // ----- SYSTEM -----
    public static event Action OnButtonPressed;


    private void Start()
    {
        _initialScale = gameObject.transform.localScale;
    }


    public void OnPointerDown(PointerEventData eventData) => Pressed();
    public void OnPointerUp(PointerEventData eventData) => UnPressed();

    private void Pressed()
    {
        LeanTween.scale(gameObject, _initialScale * _pressedScaleRate, _duration);
        if(!isMuted) OnButtonPressed?.Invoke();
    }
    private void UnPressed() => LeanTween.scale(gameObject, _initialScale, _duration);
}

