using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "GameData/PlayerDatas")]
public class GameData : ScriptableObject
{
    public float totalGold;
    public List<GemData> gemList;
    [Serializable]
    public class GemData
    {
        public string Gem_Name;
        public float Gem_Price;
        public int CollectedCount;
    }

}
