using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

    private static float playerRecord;
    
    private int _nowLvlIndex;
    private int _sceneCount;

    private void Start()
    {
        _sceneCount = levelsData.levelDatas.Length + 2;
        StartCoroutine(PlayerData.LoadYandexData(() =>
        {
            InstButts();
            ChooseLvl(2, levelsData.levelDatas[0]);
        }, UpdatePlayerRecords));
    }

    private void UpdatePlayerRecords()
    {
        var allLvlPassed = YandexGame.savesData.records[0] == 0 || YandexGame.savesData.records[1] == 0 ||
                           YandexGame.savesData.records[2] == 0;

        if (!allLvlPassed)
        {
            var newPlayerRecord = (YandexGame.savesData.records[0]
                                         + YandexGame.savesData.records[1] + YandexGame.savesData.records[2]);
            if (newPlayerRecord != playerRecord)
            {
                playerRecord = newPlayerRecord;
                            YandexGame.NewLeaderboardScores("TimeScore", (int)(playerRecord*10));
                            YandexGame.GetLeaderboard("TimeScore",
                                Int32.MaxValue, Int32.MaxValue, Int32.MaxValue, "nonePhoto");
            };
        }

        var totalText = GameTexts.recordTextResult.GetText() + (allLvlPassed
            ? "-"
            : YandexGame.savesData.records[0] + YandexGame.savesData.records[1]
                                              + YandexGame.savesData.records[2]);
        playerRecordInfoText.text = GameTexts.recordTextStart.GetText()
                                    + "\n 1: - " + (YandexGame.savesData.records[0] == 0
                                        ? GameTexts.recordTextNotRecord.GetText()
                                        : (Mathf.Round(YandexGame.savesData.records[0] * 10) / 10f))
                                    + "\n 2: - " + (YandexGame.savesData.records[1] == 0
                                        ? GameTexts.recordTextNotRecord.GetText()
                                        : Mathf.Round(YandexGame.savesData.records[1] * 10) / 10f)
                                    + "\n 3: - " + (YandexGame.savesData.records[2] == 0
                                        ? GameTexts.recordTextNotRecord.GetText()
                                        : Mathf.Round(YandexGame.savesData.records[2] * 10) / 10f)
                                    + "\n" + totalText
                                    + "\n" + GameTexts.recordTextPlace.GetText() +
                                    (!allLvlPassed ? PlayerData.playerRank : "-");
    }

    public void ChooseLvl(int lvl, LevelData levelData)
    {
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
        recordInfoText.SetActive(levelData.isForResord);
        nowLvl.text = GameTexts.lvlText.GetText() + " " + (lvl-1);
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
}
