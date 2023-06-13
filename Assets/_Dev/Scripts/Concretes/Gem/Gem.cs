using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public float scale;
    public GameData.GemData GemData{ get; set; }
    GameObject body;
    void Awake()
    {
        body = transform.GetChild(0).gameObject;
    }
    void Start()
    {
        SetBodyLocalScale(Vector3.zero);
    }
    void Update()
    {
        SetBodyLocalScale(Vector3.one * scale);
    }
    void SetBodyLocalScale(Vector3 targetScale)
    {
        body.transform.localScale = targetScale;
    }
}
