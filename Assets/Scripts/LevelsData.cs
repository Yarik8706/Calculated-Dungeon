using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "LevelsData", fileName = "LevelsData")]
public class LevelsData : ScriptableObject
{
    public LevelData[] levelDatas;
}