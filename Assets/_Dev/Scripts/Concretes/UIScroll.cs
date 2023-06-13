using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScroll : MonoBehaviour
{
    [SerializeField] GameObject InstantiateInfoGroup;
    [SerializeField] RectTransform contentBackGround;
    [SerializeField] Text scrollButtonText;
    Animator animator;
    GemSpawner _gemSpawner;
    bool animState;
    void OnEnable()
    {
        EventManager.UpdateScroll += UpdateScroll;
    }
    void OnDisable()
    {
        EventManager.UpdateScroll -= UpdateScroll;
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        _gemSpawner = ObjectManager.GemSpawner;
        animState = true;
        ChangeStateButton();
        EventManager.UpdateScroll();
    }

    void UpdateScroll()
    {
        var cCounter = _gemSpawner.GemDataList.Count - contentBackGround.transform.childCount;
        if (cCounter > 0)
        {
            for (int i = 0; i < cCounter; i++) 
            {
                var created = Instantiate(InstantiateInfoGroup, contentBackGround);
                RectTransform rect = created.GetComponent<RectTransform>();
                rect.transform.localPosition = new Vector3(0, rect.GetSiblingIndex() * -100, 0);
            }
        }
        else if(cCounter<0)
        {
            for (int i = 0; i <Mathf.Abs(cCounter); i++)
            {
                var length = contentBackGround.transform.childCount;
                DestroyImmediate(contentBackGround.transform.GetChild(length-1).gameObject);
            }
        }
        var childCount = contentBackGround.transform.childCount;
        for (int i = 0; i <childCount; i++)
        {
            contentBackGround.transform.GetChild(i).TryGetComponent<UIGemInfo>(out UIGemInfo info);
            var icon = _gemSpawner.GemDataList[i].sprite;
            var name = _gemSpawner.GemDataList[i].data.Gem_Name;
            var collectedCount = _gemSpawner.GemDataList[i].data.CollectedCount;

            info.UpdateInfo(icon, name, collectedCount);
        }
        contentBackGround.sizeDelta =new Vector2(contentBackGround.sizeDelta.x,contentBackGround.childCount * 100);
    }
    public void ChangeStateButton()
    {
        animState = !animState;
        animator.SetBool("Active", animState);
        if(animState)
        {
            scrollButtonText.text = "OFF";
        }
        else
        {
            scrollButtonText.text = "ON";
        }
    }
}
