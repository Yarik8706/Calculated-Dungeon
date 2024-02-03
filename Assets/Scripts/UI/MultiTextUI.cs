using System;
using TMPro;
using UnityEngine;

public class MultiTextUI : MonoBehaviour
{
    [SerializeField] private string ruText;
    [SerializeField] private string enText;

    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _text.text = Constants.isRu ? ruText : enText;
    }
}