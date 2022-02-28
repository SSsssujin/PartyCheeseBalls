using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Transform player;
    PlayerMovement playerMovement;
    CameraMove getCameraInfo;
    
    GameObject panel;

    GameObject purpleBox;
    Image[] purpleBoxImg;

    GameObject orangeBox;
    Image[] orangeBoxImg;

    GameObject listBox;
    Image[] listBoxImg;

    GameObject naviBox;
    Image[] naviBoxImg;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        getCameraInfo = FindObjectOfType<CameraMove>();

        panel = GameObject.Find("Tutorial Panel");

        purpleBox = GameObject.Find("Purple Info");
        purpleBoxImg = purpleBox.GetComponentsInChildren<Image>();
        for (int i = 0; i < purpleBoxImg.Length; i++)
        {
            purpleBoxImg[i].enabled = false;
        }

        orangeBox = GameObject.Find("Orange Info");
        orangeBoxImg = orangeBox.GetComponentsInChildren<Image>();
        for (int i = 0; i < orangeBoxImg.Length; i++)
        {
            orangeBoxImg[i].enabled = false;
        }

        listBox = GameObject.Find("List Info");
        listBoxImg = listBox.GetComponentsInChildren<Image>();
        for (int i = 0; i < listBoxImg.Length; i++)
        {
            listBoxImg[i].enabled = false;
        }

        naviBox = GameObject.Find("Navi Info");
        naviBoxImg = naviBox.GetComponentsInChildren<Image>();
        for (int i = 0; i < naviBoxImg.Length; i++)
        {
            naviBoxImg[i].enabled = false;
        }
    }

    void Update()
    {
        // 백뷰로 내려왔음
        if (getCameraInfo.isStarted)
        {
            // 1. Purple Box Active
            for (int i = 0; i < purpleBoxImg.Length; i++)
            {
                purpleBoxImg[i].enabled = true;

                if (purpleBoxImg[i].enabled)
                    panel.SetActive(true);
                if (!purpleBox.activeInHierarchy)    
                    panel.SetActive(false);
            }

            // 2. Orange Box Active
            if (Vector3.Distance(player.position, playerMovement.wayArr[2, 1].position) < 1)
            {
                for (int i = 0; i < orangeBoxImg.Length; i++)
                {
                    //panel.SetActive(true);
                    orangeBoxImg[i].enabled = true;

                    if (orangeBoxImg[i].enabled)
                        panel.SetActive(true);
                    if (!orangeBox.activeInHierarchy)
                        panel.SetActive(false);
                }
            }

            // 3. List Box
            if (Vector3.Distance(player.position, playerMovement.wayArr[0, 1].position) < 1)
            {
                for (int i = 0; i < listBoxImg.Length; i++)
                {
                    //panel.SetActive(true);
                    listBoxImg[i].enabled = true;

                    if (listBoxImg[i].enabled)
                        panel.SetActive(true);
                    if (!listBox.activeInHierarchy)
                        panel.SetActive(false);
                }
            }

            // 4. Navi Box
            if (Vector3.Distance(player.position, playerMovement.wayArr[0, 1].position) < 1 && !listBox.activeInHierarchy)
            {
                for (int i = 0; i < naviBoxImg.Length; i++)
                {
                    naviBoxImg[i].enabled = true;
            
                    if (naviBoxImg[i].enabled)
                        panel.SetActive(true);
                    if (!naviBox.activeInHierarchy)
                        panel.SetActive(false);
                }
            }
        } 
    }
}
