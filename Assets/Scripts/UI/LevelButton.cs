using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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

public class LevelButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject isLevelForRecord;
    [SerializeField] private GameObject levelCompleteImage;
    [SerializeField] private Image image;
    [SerializeField] private Sprite baseSprite;
    [SerializeField] private Sprite playSprite;

    private TMP_Text _text;
    private StartSceneMenu _startSceneMenu;
    private int _levelIndex;

    private void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
    }

    private LevelData LevelData { get; set; }
    
    public void Init(LevelData levelData, int levelIndex, StartSceneMenu startSceneMenu)
    {
        _startSceneMenu = startSceneMenu;
        _text.text = (levelIndex-1) + "";
        if (PlayerData.levelsPassedIndexs != null && PlayerData.levelsPassedIndexs.Any(i => i == levelIndex-1))
        {
            levelCompleteImage.SetActive(true);
        }
        _levelIndex = levelIndex-1;
        LevelData = levelData;
        // isLevelForRecord.SetActive(levelData.isForResord);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (image.sprite == playSprite)
        {
            _startSceneMenu.StartLvl();
            return;
        }

        _startSceneMenu.lvlInfo.ChangeActive(true);
        _startSceneMenu.ChooseLvl(_levelIndex, LevelData, this);
        image.sprite = playSprite;
        _text.gameObject.SetActive(false);
    }

    public void SetNotPlayIcon()
    {
        image.sprite = baseSprite;
        _text.gameObject.SetActive(true);
    }
}