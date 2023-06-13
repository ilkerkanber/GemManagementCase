using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : ASingleton<CanvasManager>
{
    [SerializeField] TextMeshProUGUI moneyText;
    public FloatingJoystick _floatingJoystick;
    void OnEnable()
    {
        EventManager.UpdateMoney += UpdateMoneyText;
    }
    void OnDisable()
    {
        EventManager.UpdateMoney -= UpdateMoneyText;
    }
    void Awake()
    {
        SetupSingleton(this);
    }
    void UpdateMoneyText(float money)
    {
        int moneyInt = (int)money;
        moneyText.text = moneyInt.ToString();
    }
}
