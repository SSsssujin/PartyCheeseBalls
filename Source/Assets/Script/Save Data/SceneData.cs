using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public int sceneNum;

    // ������ ����
    // ��ư ���� �޾ƿ� �ָ� ��������ߵ�
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

    // ������ ����
    // ��ư ���� �޾ƿ� �ָ� ��������ߵ�
    public PlayerData (Player player)
    {
        level = player.level;
        health = player.health;
    }
}
*/
