using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public int sceneNum;

    // 생성자 ㅇㅇ
    // 무튼 정보 받아올 애를 선언해줘야됨
    public SceneData(SceneManagement scene)
    {
        //sceneNum = scene.curScene;
    }
}

/*
[System.Serializable]
public class SceneData
{
    public int level;
    public int health;
    public float[] position;

    // 생성자 ㅇㅇ
    // 무튼 정보 받아올 애를 선언해줘야됨
    public PlayerData (Player player)
    {
        level = player.level;
        health = player.health;
    }
}
*/
