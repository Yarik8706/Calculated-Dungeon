using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UI;
using UnityEngine.SceneManagement;
using YG;

public class StartSceneMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text nowLvl;
    [SerializeField] private GameObject lvlButton;
    [SerializeField] private LevelsData levelsData;
    [SerializeField] private Transform spawn;
    [SerializeField] private TMP_Text levelNameText;
    [SerializeField] private TMP_Text difficultTypeText;
    [SerializeField] private GameObject recordInfoText;
    [SerializeField] private TMP_Text playerRecordInfoText;
    [SerializeField] private ActiveElement[] lvlsObjects;
    public ActiveElement lvlInfo; 

    private static float playerRecord;

    private int _nowLvlIndex;
    private LevelButton _activeLevelButton;
    private int _sceneCount;
    private bool _lvlUiActive;

    private void Start()
    {
        _sceneCount = levelsData.levelDatas.Length + 2;
        InstButts();
    }

    public void ChooseLvl(int lvl, LevelData levelData, LevelButton levelButton)
    {
        if(_activeLevelButton != null) _activeLevelButton.SetNotPlayIcon();
        _activeLevelButton = levelButton;
        _nowLvlIndex = lvl;
        switch (levelData.levelDifficult)   
        {
            case LevelDifficult.Easy:
                difficultTypeText.color = Color.green;
                difficultTypeText.text = GameTexts.easyLvlText.GetText();
                break;
            case LevelDifficult.Middle:
                difficultTypeText.color = Color.yellow;
                difficultTypeText.text = GameTexts.middleLvlText.GetText();
                break;
            case LevelDifficult.Hard:
                difficultTypeText.color = Color.red;
                difficultTypeText.text = GameTexts.hardLvlText.GetText();
                break;
            default:
                difficultTypeText.color = Color.yellow;
                difficultTypeText.text = GameTexts.middleLvlText.GetText();
                break;
        }
        levelNameText.text = levelData.levelName.GetText();
        // recordInfoText.SetActive(levelData.isForResord);
        recordInfoText.SetActive(false);
        // nowLvl.text = GameTexts.lvlText.GetText() + " " + (lvl-1);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void StartLvl()
    {
        BlackoutControl.Instance.StartBlackount().OnComplete(() =>
        {
            SceneManager.LoadScene(_nowLvlIndex);
        });
    }

    private void InstButts()
    {
        for (int sceneIndex = 2; sceneIndex < _sceneCount; sceneIndex++)
        {
            var levelButton = Instantiate(lvlButton, spawn).GetComponent<LevelButton>();
            
            levelButton.Init(levelsData.levelDatas[sceneIndex-2], sceneIndex, this);
        }
    }

    public void PlayButton()
    {
        lvlInfo.ChangeActive(false);
        _lvlUiActive = !_lvlUiActive;
        foreach (var activeElement in lvlsObjects)
        {
            activeElement.ChangeActive(_lvlUiActive);
        }
    }
}
