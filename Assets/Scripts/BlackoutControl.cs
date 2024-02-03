using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BlackoutControl : MonoBehaviour
{
    private Image _blackoutImage;

    public static BlackoutControl Instance { get; private set; }
    public static Color activeColorState = Color.black;

    private void Awake()
    {
        Instance = this;
        _blackoutImage = GetComponent<Image>();
    }

    private void Start()
    {
        EndBlackout();
    }

    private void OnDestroy()
    {
        activeColorState = _blackoutImage.color;
    }

    public Tween StartBlackount()
    {
        return _blackoutImage.DOFade(1f, 0.8f).SetLink(gameObject);
    }

    public Tween EndBlackout()
    {
        _blackoutImage.color = activeColorState;
        return _blackoutImage.DOFade(0, 0.8f).SetLink(gameObject);
    }
}