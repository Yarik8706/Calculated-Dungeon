using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExpressionSymbol : MonoBehaviour
{
    public GameObject postSymbol;
    public bool canIBeDestroyed = true;

    public TMP_Text Text { get; private set; }

    private void Awake()
    {
        Text = GetComponentInChildren<TMP_Text>();
    }

    public void DestrMe()
    {
        if (!canIBeDestroyed) return;
        GameObject asd = Instantiate(postSymbol, transform.position, Quaternion.identity);//Quaternion.identity
        Destroy(asd, 2f);
        Destroy(gameObject);
    }

    public void StartDarness()
    {
        StartCoroutine(StartDarnessCoroutine());
    }

    private IEnumerator StartDarnessCoroutine()
    {
        int len = 10 * Random.Range(1, 14);
        SpriteRenderer sr = transform.GetComponentInChildren<SpriteRenderer>();
        for (int i = 0; i < len; i++)
        {
            float con = Mathf.Lerp(0.2f, 1, 1 - (float)(i + 1) / len);
            sr.color = new Color(sr.color.r * con,
                sr.color.g * con,
                sr.color.b * con,
                1);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
