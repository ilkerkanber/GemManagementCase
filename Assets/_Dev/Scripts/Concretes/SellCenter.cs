using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class SellCenter : MonoBehaviour
{
    public Transform GemPoint;
    public float TotalGold;
    GemSpawner _gemSpawner;
    GameData _gameData;
    Vector3 fScale;
    GameObject meshObject;

    void Awake()
    {
        meshObject = transform.GetChild(0).gameObject;
        ObjectManager.SellCenter = this;
    }
    void Start()
    {
        _gemSpawner = ObjectManager.GemSpawner;
        _gameData = _gemSpawner.data;
        fScale = meshObject.transform.localScale;
        TotalGold = _gameData.totalGold;
        EventManager.UpdateMoney(TotalGold);
    }
    public void Completed(Gem gem)
    {
        var price = gem.GemData.Gem_Price + (gem.scale * 100);
        TotalGold += price;
        _gameData.totalGold = TotalGold;
        SearchAndIncrease(gem.GemData.Gem_Name);
        EventManager.UpdateMoney(TotalGold);
        EventManager.SaveData();
        EventManager.UpdateScroll();
        Blob();
    }
    void SearchAndIncrease(string gemName)
    {
        foreach (var item in _gemSpawner.GemDataList)
        {
            if (item.data.Gem_Name == gemName)
            {
                item.data.CollectedCount++;
                break;
            }
        }
    }
    void Blob()
    {
        var seq = DOTween.Sequence();
        seq.Append(meshObject.transform.DOScale(fScale * 1.2f, 0.05f));
        seq.Append(meshObject.transform.DOScale(fScale, 0.05f));
    }
}
