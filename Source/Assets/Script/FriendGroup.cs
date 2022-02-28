using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendGroup : MonoBehaviour
{
    PlayerMovement playerMovement;
    GameManager gameManager;

    public GameObject[] friends;

    private void Start()
    {
        // Player Move Num
        playerMovement = FindObjectOfType<PlayerMovement>();

        // Game Manager
        gameManager = FindObjectOfType<GameManager>();

        // Friend Off
        for (int i = 0; i < friends.Length; i++)
        {
            friends[i].SetActive(true);
        }
    }

    private void Update()
    {
        if (gameManager.isDestination)
        {
            SetFriends();
        }

    }

    private void SetFriends()
    {
        // 0~1
        if (playerMovement.moveNum >= 0 && playerMovement.moveNum <= 1)
        {
            for (int i = 0; i < friends.Length; i++)
            {
                friends[i].SetActive(false);
            }
        }
        // 2
        else if (playerMovement.moveNum == 2)
        {
            for (int i = 2; i < friends.Length; i++)
            {
                friends[i].SetActive(false);
            }
        }
        // 3~5
        else if (playerMovement.moveNum >= 3 && playerMovement.moveNum <= 5)
        {
            for (int i = 5; i < friends.Length; i++)
            {
                friends[i].SetActive(false);
            }
        }
        // 6~7
        else if (playerMovement.moveNum >= 6 && playerMovement.moveNum <= 7)
        {
            for (int i = 7; i < friends.Length; i++)
            {
                friends[i].SetActive(false);
            }
        }
        // 8~
        else if (playerMovement.moveNum >= 8)
        {

        }
    }
}
