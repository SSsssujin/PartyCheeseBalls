using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CabbageParty : MonoBehaviour
{
    private Transform[] cabbages;
    private float       fadeTime = 2f;
    private Image[]     cabbageImg;

    void OnEnable()
    {
        cabbageImg = gameObject.GetComponentsInChildren<Image>();
        cabbages = gameObject.GetComponentsInChildren<Transform>();

        ControlScale();
        StartCoroutine(FadeInOut());
    }

    private void ControlScale()
    {

        // 양배추 오브젝트 0.
        iTween.ScaleTo(cabbages[1].gameObject,
            iTween.Hash("x", 0.3, "y", 0.3, //"z", 0.3,
            "easetype", "linear",
            "looptype", "pingpong"));

        // 양배추 오브젝트 1.
        iTween.ScaleTo(cabbages[2].gameObject,
            iTween.Hash("x", 2, "y", 2, //"z", 2,
            "easetype", "linear",
            "looptype", "pingpong"));
        
        // 양배추 오브젝트 2.
        iTween.ScaleTo(cabbages[3].gameObject,
            iTween.Hash("x", 1.3f, "y", 1.3f, //"z", 1.3f,
            "easetype", "linear",
            "looptype", "pingpong"));
        
        // 양배추 오브젝트 3.
        iTween.ScaleTo(cabbages[4].gameObject,
            iTween.Hash("x", 2, "y", 2,// "z", 2,
            "easetype", "linear",
            "looptype", "pingpong"));
        
        // 양배추 오브젝트 4.
        iTween.ScaleTo(cabbages[5].gameObject,
            iTween.Hash("x", 0.4f, "y", 0.4f, //"z", 0.4f,
            "easetype", "linear",
            "looptype", "pingpong"));
        
        // 양배추 오브젝트 5.
        iTween.ScaleTo(cabbages[6].gameObject,
            iTween.Hash("x", 2, "y", 2,// "z", 2,
            "easetype", "linear",
            "looptype", "pingpong"));
        
        // 양배추 오브젝트 6.
        iTween.ScaleTo(cabbages[7].gameObject,
            iTween.Hash("x", 0.5f, "y", 0.5f,// "z", 0.5f,
            "easetype", "linear",
            "looptype", "pingpong"));
        
        // 양배추 오브젝트 7.
        iTween.ScaleTo(cabbages[8].gameObject,
             iTween.Hash("x", 2.3f, "y", 2.3f, //"z", 2.3f,
             "easetype", "linear",
             "looptype", "pingpong"
             /*"delay", 0, "time", 0.7f*/));
        
        // 양배추 오브젝트 8.
        iTween.ScaleTo(cabbages[9].gameObject,
            iTween.Hash("x", 0.1f, "y", 0.1f, //"z", 0.1f,
            "easetype", "linear",
            "looptype", "pingpong"
            /*"delay", 0, "time", 0.7f*/));
    }


    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (currentTime < 4f)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;           // 나누기 안해주면 무조건 1초동안 실행됨

            for (int i = 0; i < cabbageImg.Length; i++)
            {
                Color color = cabbageImg[i].color;
                color.a = Mathf.Lerp(start, end, percent);
                cabbageImg[i].color = color;
            }

            yield return null;
        }
    }

    private IEnumerator FadeInOut()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(0, 1));    // Fade Out
            yield return StartCoroutine(Fade(1, 0));    // Fade In

            break;
        }
       
        gameObject.SetActive(false);
    }

}
