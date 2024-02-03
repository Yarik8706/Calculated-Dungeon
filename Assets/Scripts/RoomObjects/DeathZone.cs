using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        EndLevelControl.Instance.EndLvl(false, 
            GameTexts.DeathTexts[Random.Range(0, GameTexts.DeathTexts.Length)].GetText());
        PlayerControl.Instance.Die();
    }
}
