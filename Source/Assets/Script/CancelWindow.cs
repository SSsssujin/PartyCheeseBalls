using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelWindow : MonoBehaviour
{
    GameObject panel;

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
