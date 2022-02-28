using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    private GameObject settingWindow;

    private void Start()
    {
        settingWindow = GameObject.Find("Setting Window");
        settingWindow.SetActive(false);
    }

    public void OnSettingWindow()
    {
        settingWindow.SetActive(true);
    }

    public void Close()
    {
        settingWindow.SetActive(false);
    }


    public void OffBGM()
    {
        Toggle BGMtoggle = GameObject.Find("BGM").GetComponent<Toggle>();
        AudioSource getBGM = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        if (BGMtoggle.isOn) getBGM.enabled = true;
        else getBGM.enabled = false;
    }

    public void OffSound()
    {
        Toggle soundToggle = GameObject.Find("Sound").GetComponent<Toggle>();
        SoundMute soundMute = GameObject.Find("GameManager").GetComponent<SoundMute>();

        if (soundToggle.isOn) soundMute.enabled = false;
        else soundMute.enabled = true;
    }

    public void TopView()
    {
        CameraMove camMove = FindObjectOfType<CameraMove>();

    }
}
