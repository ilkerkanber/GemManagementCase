using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager 
{
    public static Action<float> UpdateMoney;
    public static Action UpdateScroll;
    public static Action SaveData;
}
