using System;
using DG.Tweening;
using RoomObjects;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WinterGlow : LightFlashes
{
    private GameObject _glow;

    protected override void Start()
    {
        _glow = transform.GetChild(0).gameObject;
        _glow.SetActive(false);
        base.Start();
    }

    protected override void ResetAndChangeLightIntensity(float x)
    {
        _glow.SetActive(x > 0);
        DOTween.To(() => _light2D.pointLightOuterRadius, 
            v => base.ResetAndChangeLightIntensity(v), x, 1.5f);
    }
}