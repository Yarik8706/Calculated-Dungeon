using System;
using TMPro;
using UnityEngine;


namespace UI
{
    public class LevelTextControl : MonoBehaviour
    {
        private TMP_Text _text;

        private void Start()
        {
            _text = GetComponent<TMP_Text>();
            _text.text = GameTexts.TextsOnLvls[LevelControl.SceneId].GetText();
        }
    }
}