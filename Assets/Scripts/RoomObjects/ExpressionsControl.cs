using System;
using RoomObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExpressionsControl : MonoBehaviour
{
    public int UnsolvedExpressionCount { get; set; }

    public static ExpressionsControl Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void AddSolvedExpressionCount()
    {
        UnsolvedExpressionCount--;
        if (UnsolvedExpressionCount == 0)
        {
            Exit.Instance.OpenDoor();
        }
    }
}
