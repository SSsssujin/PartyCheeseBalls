using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveIndicator : MonoBehaviour
{
    Image indicatorImg;

    void Start()
    {
        indicatorImg = GetComponent<Image>();
    }

    public void Active()
    {
        indicatorImg.enabled = true;
    }

    public void Inactive()
    {
        indicatorImg.enabled = false;
    }
}
