using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum LevelDifficult
{
    Easy,
    Middle,
    Hard
}

public interface IMultiText
{
    
    
    public string GetText();
}

[Serializable]
public struct MultiText : IMultiText
{
    public string ruText;
    public string enText;

    public string GetText()
    {
        return Constants.isRu ? ruText : enText;
    }

    public MultiText(string ruText, string enText)
    {
        this.ruText = ruText;
        this.enText = enText;
    }
}

public struct CrossplatformMultiText : IMultiText
{
    public MultiText phoneText;
    public MultiText pcText;

    public CrossplatformMultiText(MultiText phoneText, MultiText pcText)
    {
        this.phoneText = phoneText;
        this.pcText = pcText;
    }

    public string GetText()
    {
        return Constants.isPC ? pcText.GetText() : phoneText.GetText();
    }
}

[Serializable]
public class LevelData
{
    public bool isForResord;
    public MultiText levelName;
    public LevelDifficult levelDifficult;
}

public class LevelButton : MonoBehaviour
{
    [SerializeField] private GameObject isLevelForRecord;
    [SerializeField] private GameObject levelCompleteImage;

    private TMP_Text _text;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _text = GetComponentInChildren<TMP_Text>();
    }

    private LevelData LevelData { get; set; }
    
    public void Init(LevelData levelData, int levelIndex, StartSceneMenu startSceneMenu)
    {
        _text.text = (levelIndex-1) + "";
        if (PlayerData.levelsPassedIndexs != null && PlayerData.levelsPassedIndexs.Any(i => i == levelIndex-1))
        {
            levelCompleteImage.SetActive(true);
        }
        var index = levelIndex;
        _button.onClick.AddListener(() =>
        {
            startSceneMenu.ChooseLvl(index, LevelData);
        });
        LevelData = levelData;
        isLevelForRecord.SetActive(levelData.isForResord);
    }
}