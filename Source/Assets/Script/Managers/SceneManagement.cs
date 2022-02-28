using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    GameManager gameManager;

    //int activeScene;
    bool isPaused;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // ���� ������ �� �� ��ȣ ����
    private void OnApplicationQuit()
    {
        //int activeScene = SceneManager.GetActiveScene().buildIndex;
        //
        //PlayerPrefs.SetInt("ActiveScene", activeScene);
        
        int activeScene;

        if (gameManager.isClear)
        {
            activeScene = SceneManager.GetActiveScene().buildIndex + 1;
        }
        else
        {
            activeScene = SceneManager.GetActiveScene().buildIndex;
        }
        PlayerPrefs.SetInt("ActiveScene", activeScene);
    }

    private void OnApplicationPause(bool pause)
    {
        isPaused = pause;
        int activeScene;

        if (isPaused)
        {
            if (gameManager.isClear)
            {
                activeScene = SceneManager.GetActiveScene().buildIndex + 1;
            }
            else
            {
                activeScene = SceneManager.GetActiveScene().buildIndex;
            }
            PlayerPrefs.SetInt("ActiveScene", activeScene);
        }
    }

    public void LoadScene()
    {
        //PlayerPrefs.DeleteAll();

        int activeScene = PlayerPrefs.GetInt("ActiveScene");

        Debug.Log(activeScene);

        if (activeScene == 0)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            //activeScene = PlayerPrefs.GetInt("ActiveScene");
            StartCoroutine(LoadNewScene(activeScene));
        }
         

        //SceneManager.LoadScene(activeScene);

        //Note: In most cases, to avoid pauses or performance hiccups while loading,
        //you should use the asynchronous version of the LoadScene() command which is: LoadSceneAsync()

        //Loads the Scene asynchronously in the background
        //StartCoroutine(LoadNewScene(activeScene));
    }

    IEnumerator LoadNewScene(int sceneBuildIndex)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneBuildIndex);
        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.progress < 0.9f)
        {
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;
    }

    // =======================================
    // �������� ----> Next Scene �Ѿ�� ���
    public void LoadNextScene()
    {
        // ���� ���� ������ ������ �´�.
        Scene scene = SceneManager.GetActiveScene();

        // ���� ���� ���� ������ ������ �´�.
        //int curScene = scene.buildIndex;
        int curScene = scene.buildIndex;

        // ���� ���� �ٷ� ���� ���� �������� ���� +1�� ���ش�.
        int nextScene = curScene + 1;

        // ���� ���� �ҷ��´�.
        SceneManager.LoadScene(nextScene);
    }
}
