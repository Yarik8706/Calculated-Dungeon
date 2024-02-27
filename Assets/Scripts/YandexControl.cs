using UnityEngine;

public class YandexControl : MonoBehaviour
{
    // private void UpdatePlayerRecords()
    // {
    //     var allLvlPassed = YandexGame.savesData.records[0] == 0 || YandexGame.savesData.records[1] == 0 ||
    //                        YandexGame.savesData.records[2] == 0;
    //
    //     if (!allLvlPassed)
    //     {
    //         var newPlayerRecord = (YandexGame.savesData.records[0]
    //                                      + YandexGame.savesData.records[1] + YandexGame.savesData.records[2]);
    //         if (newPlayerRecord != playerRecord)
    //         {
    //             playerRecord = newPlayerRecord;
    //                         YandexGame.NewLeaderboardScores("TimeScore", (int)(playerRecord*10));
    //                         YandexGame.GetLeaderboard("TimeScore",
    //                             Int32.MaxValue, Int32.MaxValue, Int32.MaxValue, "nonePhoto");
    //         };
    //     }
    //
    //     var totalText = GameTexts.recordTextResult.GetText() + (allLvlPassed
    //         ? "-"
    //         : YandexGame.savesData.records[0] + YandexGame.savesData.records[1]
    //                                           + YandexGame.savesData.records[2]);
    //     playerRecordInfoText.text = GameTexts.recordTextStart.GetText()
    //                                 + "\n 1: - " + (YandexGame.savesData.records[0] == 0
    //                                     ? GameTexts.recordTextNotRecord.GetText()
    //                                     : (Mathf.Round(YandexGame.savesData.records[0] * 10) / 10f))
    //                                 + "\n 2: - " + (YandexGame.savesData.records[1] == 0
    //                                     ? GameTexts.recordTextNotRecord.GetText()
    //                                     : Mathf.Round(YandexGame.savesData.records[1] * 10) / 10f)
    //                                 + "\n 3: - " + (YandexGame.savesData.records[2] == 0
    //                                     ? GameTexts.recordTextNotRecord.GetText()
    //                                     : Mathf.Round(YandexGame.savesData.records[2] * 10) / 10f)
    //                                 + "\n" + totalText
    //                                 + "\n" + GameTexts.recordTextPlace.GetText() +
    //                                 (!allLvlPassed ? PlayerData.playerRank : "-");
    // }
    // StartCoroutine(PlayerData.LoadYandexData(() =>
    // {
    //     InstButts();
    //     ChooseLvl(2, levelsData.levelDatas[0]);
    // }, UpdatePlayerRecords));
}