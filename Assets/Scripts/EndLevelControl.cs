using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using YG;

public class EndLevelControl : MonoBehaviour
{
    public TMP_Text text_not;
    public TMP_Text wl_mess;

    public AudioSource winSound;
    public AudioSource loseSound;

    public static EndLevelControl Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void EndLvl(bool isWin, string endPhrase)
    {
        if (LevelControl.Instance.IsWin || LevelControl.Instance.IsDefeat) return;
        if (isWin)
        {
            LevelControl.Instance.IsWin = true;
            if (winSound != null) winSound.Play();
            wl_mess.text = GameTexts.winText.GetText();
            YandexGame.savesData.levelPassed = YandexGame.savesData.levelPassed + (SceneManager.GetActiveScene().buildIndex - 1) + ";";
            TimerBar.Instance.SaveRecord();
            PlayerData.SaveData();
        }
        else
        {
            LevelControl.Instance.IsDefeat = true;
            if (loseSound != null) loseSound.Play();
            wl_mess.text = GameTexts.defeatText.GetText();
        }
        
        text_not.text = endPhrase;
        
        PlayerControl.Instance.Die();
    }
}
