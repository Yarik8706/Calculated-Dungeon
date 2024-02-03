using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using YG.Utils.LB;

public static class PlayerData
{
    public static int[] levelsPassedIndexs;
    public static int playerRank;

    public static IEnumerator LoadYandexData(Action startGame, Action updateRecord)
    {
        YandexGame.onGetLeaderboard += v =>
        {
            OnUpdateLB(v, updateRecord);
        };
        YandexGame.GetLeaderboard("TimeScore",
                 Int32.MaxValue, Int32.MaxValue, Int32.MaxValue, "nonePhoto");
        yield return new WaitUntil(() => YandexGame.SDKEnabled);
        GetLevelsPassedData();
        startGame.Invoke();
        updateRecord.Invoke();
    }
    
    public static void GetLevelsPassedData()
    {
        if(YandexGame.savesData.levelPassed == "") return;
        string[] levelsPassedIndexsStrings = YandexGame.savesData.levelPassed.Split(';', StringSplitOptions.RemoveEmptyEntries);
        levelsPassedIndexs = new int[levelsPassedIndexsStrings.Length];
        for (int i = 0; i < levelsPassedIndexsStrings.Length; i++)
        {
            levelsPassedIndexs[i] = Convert.ToInt32(levelsPassedIndexsStrings[i]);
        }
    }

    public static void SaveData()
    {
        YandexGame.SaveProgress();
    }

    private static void OnUpdateLB(LBData lb, Action updateRecord)
    {
        foreach (var player in lb.players)
        {
            if (player.uniqueID == YandexGame.playerId)
            {
                playerRank = player.rank;
                updateRecord.Invoke();
                return;
            }
        }

        playerRank = lb.players.Length + 1;
        updateRecord.Invoke();
    }

    public static void ChangeLanguageEvent(string lang)
    {
        if(!(lang == "ru" && Constants.isRu || lang == "en" && !Constants.isRu)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Constants.isRu = lang == "ru";
    }
}