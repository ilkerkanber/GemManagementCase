using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] float lerpSpeed;
    GameObject target;
    Vector3 offSetTarget;
    void Awake()
    {
        ObjectManager.CameraManager = this;
    }
    void Start()
    {
        target = ObjectManager.PlayerController.gameObject;
        offSetTarget = target.transform.position - transform.position;
    }
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position - offSetTarget, Time.deltaTime * lerpSpeed);    
    }

}

