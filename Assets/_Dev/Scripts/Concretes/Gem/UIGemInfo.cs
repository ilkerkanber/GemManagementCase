using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGemInfo : MonoBehaviour
{
    public Image GemImage;
    public TextMeshProUGUI GemText;
    public TextMeshProUGUI GemCountText;

    public void UpdateInfo(Sprite sprite, string name, int gemCount)
    {
        GemImage.sprite = sprite;
        GemText.text = name;
        GemCountText.text = gemCount.ToString();
    }
}
