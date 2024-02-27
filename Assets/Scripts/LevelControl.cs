using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Inputs;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour
{
    public TMP_Text startText;
    
    public bool IsGameStart { get; private set; }
    public bool IsWin { get; set; }
    public bool IsDefeat { get; set; }

    public static LevelControl Instance { get; private set; }
    public static int SceneId;

    private void Awake()
    {

        SceneId = SceneManager.GetActiveScene().buildIndex - 1;
#if UNITY_EDITOR
        Constants.isEditor = true;
#endif
        Instance = this;
    }

    private void Start()
    {
        StartScene();
    }

    private void Update()
    {
        if ((Input.touchCount > 0 || Input.anyKey) && !IsGameStart)
        {
#if !UNITY_EDITOR
            IsGameStart = true;
#endif
            if(startText != null)startText.gameObject.SetActive(false);
        }
        
        if (InputControl.Instance.restartLvl && IsDefeat)
        {
            FinishScene();
        }

        if (InputControl.Instance.nextLvl && IsWin)
        {
            NextScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            NextScene(Constants.MenuSceneId);
        }
    }

    private void StartScene()
    {
        PlayerPrefs.SetInt("NowLvl", SceneManager.GetActiveScene().buildIndex);
    }

    private void FinishScene()
    {
        BlackoutControl.Instance.StartBlackount().OnComplete(() =>
        {
           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        });
    }

    private void NextScene(int nextSceneIndex)
    {
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            IntroDialogControl.isIntro = false;
            nextSceneIndex = 0;
        }
        BlackoutControl.Instance.StartBlackount().OnComplete(() =>
        {
            SceneManager.LoadScene(nextSceneIndex);
        });
    }
}
