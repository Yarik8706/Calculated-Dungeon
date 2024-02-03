using System;
using UnityEngine;

public class ApplicationControl : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 2;
    }
}