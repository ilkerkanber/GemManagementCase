using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] List<Gem> gemList;
    [SerializeField] float PlayerSpeed;
    [SerializeField] Transform backPoint;
    
    PlayerAnimation _playerAnimation;
    FloatingJoystick floatingJoystick;

    SellCenter _sellCenter;
    Vector2 tempPos;
    Vector3 resultPos;
    float collectedTotalScaleY;
    float sellTimer;
    
    void Awake()
    {
        _playerAnimation = new PlayerAnimation(GetComponent<Animator>());
        ObjectManager.PlayerController = this;
    }
    void Start()
    {
        _sellCenter = ObjectManager.SellCenter;
        floatingJoystick = CanvasManager.Instance._floatingJoystick;
    }
    void Update()
    {
        tempPos = new Vector3(floatingJoystick.Horizontal, floatingJoystick.Vertical);
        if (tempPos == Vector2.zero)
        {
            _playerAnimation.Idle();
        }
        else if(Mathf.Abs(tempPos.x) < 0.7f && Mathf.Abs(tempPos.y) < 0.7f)
        {
            _playerAnimation.Walk();
        }
        else
        {
            _playerAnimation.Run();

        }
        LookAndMove();
    }
    void LookAndMove()
    {
        resultPos = transform.position + new Vector3(tempPos.x, 0, tempPos.y) * Time.deltaTime * PlayerSpeed;
        transform.LookAt(resultPos);
        transform.position = resultPos;
    }
    float CalculateYFormula(Gem gem)
    {
        var gemRootScaleY = gem.transform.lossyScale.y;
        var gemScaleY = gem.scale;
        var resultY = gemRootScaleY * gemScaleY * 1.5f;
        return resultY;
    }
    void SendSellCenterStart()
    {
        var lastIndex = gemList.Count-1;
        var lastGem = gemList[lastIndex];
        var lastGemYScale = CalculateYFormula(lastGem);
        lastGem.transform.parent = _sellCenter.GemPoint;
        gemList.Remove(lastGem);
        sellTimer = 0f;
        collectedTotalScaleY -= lastGemYScale;
        lastGem.transform.DOLocalJump(Vector3.zero, 1, 1, 0.2f).OnComplete(()=> _sellCenter.Completed(lastGem));
    }
    void CollectStart(Gem gem)
    {
        var resultY = CalculateYFormula(gem);
        var targetPoint = new Vector3(0, collectedTotalScaleY,0);
        gem.transform.parent = backPoint.transform;
        collectedTotalScaleY+= resultY;
        gem.transform.DOLocalRotateQuaternion(Quaternion.Euler(Vector2.zero),0.5f);
        gem.transform.DOLocalJump(targetPoint, 1f, 1, 0.5f).OnComplete(() => StartCoroutine(CollectEnd(gem)));
    }
    IEnumerator CollectEnd(Gem gem)
    {
        gemList.Add(gem);
        var listCount = gemList.Count;
        for (int i = listCount-1; i >=0; i--)
        {
            yield return new WaitForSeconds(0.05f);
            Transform gemTransform = gemList[i].transform;
            DG.Tweening.Sequence seq = DOTween.Sequence();
            seq.Append(gemTransform.DOScale(Vector3.one * 1.5f, 0.1f));
            seq.Append(gemTransform.DOScale(Vector3.one, 0.1f));
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Tile"))
        { 
            other.transform.parent.TryGetComponent<Tile>(out Tile tile);
            CollectStart(tile.Gem);
            tile.Collected();
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SellCenter"))
        {
            sellTimer += Time.deltaTime;
            if (sellTimer >= 0.1f && gemList.Count>0)
            {
                SendSellCenterStart();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SellCenter"))
        {
            sellTimer = 0f;
        }
    }
}
