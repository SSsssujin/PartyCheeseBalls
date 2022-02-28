using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial2 : MonoBehaviour
{
    public Transform player;
    PlayerMovement playerMovement;
    CameraMove getCameraInfo;

    GameObject panel;

    GameObject skyblueBox;
    Image[] skyblueBoxImg;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        getCameraInfo = FindObjectOfType<CameraMove>();

        panel = GameObject.Find("Tutorial Panel");

        skyblueBox = GameObject.Find("Skyblue Info");
        skyblueBoxImg = skyblueBox.GetComponentsInChildren<Image>();
        for (int i = 0; i < skyblueBoxImg.Length; i++)
        {
            skyblueBoxImg[i].enabled = false;
        }
    }

    private void Update()
    {
        if (getCameraInfo.isStarted)
        {
            // 1. skyblue Box Active
            for (int i = 0; i < skyblueBoxImg.Length; i++)
            {
                skyblueBoxImg[i].enabled = true;

                if (skyblueBoxImg[i].enabled)
                    panel.SetActive(true);
                if (!skyblueBox.activeInHierarchy)
                    panel.SetActive(false);
            }
        }
        }
}
