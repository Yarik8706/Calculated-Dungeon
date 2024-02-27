using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;
using Random = UnityEngine.Random;

public class TimerBar : MonoBehaviour
{
    public float timerTotal = 10f;
    public float timer;
    public Slider slider;

    [SerializeField] private TMP_Text timerText;
    [SerializeField] private int saveIndex = -1;

    private float _activeTime;

    public static TimerBar Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RestartTimer();
        timerText.gameObject.SetActive(false);
    }

    public void SaveRecord()
    {
        if(saveIndex == -1 || YandexGame.savesData.records[saveIndex] < _activeTime && YandexGame.savesData.records[saveIndex] != 0) return; 
        YandexGame.savesData.records[saveIndex] = Mathf.Round(_activeTime*10)/10f;
        PlayerData.SaveData();
    }

    private void Update()
    {
        // if (saveIndex != -1)
        // {
        //     if(LevelControl.Instance.IsDefeat || LevelControl.Instance.IsWin) return;
        //     _activeTime += Time.deltaTime;
        //     timerText.text = (Mathf.Round(_activeTime*10)/10) + "";
        // }
        if (LevelControl.Instance.IsWin || LevelControl.Instance.IsDefeat || !LevelControl.Instance.IsGameStart) return;
        slider.value = timer / timerTotal;
        timer -= Time.deltaTime;

        if (timer <= 0) RestartLvl();
    }

    private void RestartLvl()
    {
        EndLevelControl.Instance.EndLvl(false, 
            GameTexts.TimeEndTexts[Random.Range(0, GameTexts.TimeEndTexts.Length)].GetText());
    }
    
    public void RestartTimer()
    {
        timer = timerTotal;
    }
}
