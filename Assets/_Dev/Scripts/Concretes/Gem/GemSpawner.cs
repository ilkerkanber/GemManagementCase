using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class GemSpawner : MonoBehaviour
{
    public List<GemDataValue> GemDataList;
    [Serializable]
    public class GemDataValue
    {
        public GameData.GemData data;
        public Sprite sprite;
        public GameObject spawnObject;
    }
    public GameData data;
    void OnEnable()
    {
        EventManager.SaveData += SaveData;
    }
    void OnDisable()
    {
        EventManager.SaveData -= SaveData;
    }
    void Awake()
    {
        ObjectManager.GemSpawner = this;
        LoadData();
    }
    void LoadData()
    {
        DataManager.LoadData(data);
        for (int i = 0; i < data.gemList.Count; i++)
        {
            GemDataList[i].data = data.gemList[i];
        }
    }
    void SaveData()
    {
        if(data.gemList.Count == GemDataList.Count) 
        {
            for (int i = 0; i < GemDataList.Count; i++)
            {
                data.gemList[i] = GemDataList[i].data;
            }
        }
        else
        {
            data.gemList.Clear();
            for (int i = 0; i < GemDataList.Count; i++)
            {
                data.gemList.Add(GemDataList[i].data);
            }
        }
        DataManager.SaveData(data);
        if(Application.isPlaying)
        {
            EventManager.UpdateScroll();
        }
    }
}



