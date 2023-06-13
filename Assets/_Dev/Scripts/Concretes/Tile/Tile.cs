using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public float respawnDelay;
    public float scaleTimer;
    public Gem Gem { get; private set; }
    GemSpawner _gemSpawner;
    float tempScaleTimer;
    bool IsFull;
    Collider trigger;
    void Awake()
    {
        trigger = transform.GetChild(0).GetComponent<Collider>(); 
    }
    void Start()
    {
        _gemSpawner = ObjectManager.GemSpawner;
        StartCoroutine(GetRandomGem(UnityEngine.Random.Range(0, 2f)));
    }
    void Update()
    {
        if(IsFull)
        {
            tempScaleTimer = scaleTimer + Time.deltaTime/5f;
            tempScaleTimer = Mathf.Clamp(tempScaleTimer, 0, 1f);
            scaleTimer = tempScaleTimer;
            Gem.scale = tempScaleTimer;
            if (scaleTimer > 0.25f)
            {
                trigger.enabled = true;
            }
        }
    }
    IEnumerator GetRandomGem(float delay)
    {
        yield return new WaitForSeconds(delay);
        var RandomNumber = UnityEngine.Random.Range(0, _gemSpawner.GemDataList.Count);
        var gemProperties = _gemSpawner.GemDataList[RandomNumber];
        var tempObject = gemProperties.spawnObject;
        var spawnObj = Instantiate(tempObject, transform.position, Quaternion.identity, transform);
        Gem = spawnObj.GetComponent<Gem>();
        Gem.GemData = gemProperties.data;
        Gem.scale = 0f;
        scaleTimer = 0f;
        IsFull = true;
    }
    public void Collected()
    {
        IsFull = false;
        trigger.enabled = false;
        scaleTimer = 0f;
        //RESTART
        StartCoroutine(GetRandomGem(respawnDelay));
    }
}
