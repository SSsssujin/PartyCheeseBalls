using UnityEngine;
using System.IO;        // Creating and Opening the actual save file
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    // 인스턴스화 하지 않고도 어디에서나 호출될 수 있게 (Static)
    public static void SaveScene(SceneManagement scene)
    {
        // Create a binary format
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/scene.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        // 생성자로 데이터 클래스가 자체적으로 설정됨
        SceneData data = new SceneData(scene);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SceneData LoadScene()
    {
        string path = Application.persistentDataPath + "/scene.fun";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SceneData data = formatter.Deserialize(stream) as SceneData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}


/*
// 동일한 다른 클래스가 생성되지 않도록 Static 키워드를 사용
public static class SaveSystem
{
    // 인스턴스화 하지 않고도 어디에서나 호출될 수 있게 (Static)
    public static void SavePlayer (Player player)
    {
        // 1. Create a binary format
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        // 생성자로 데이터 클래스가 자체적으로 설정됨
        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
*/
