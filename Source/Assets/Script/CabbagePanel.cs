using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CabbagePanel : MonoBehaviour
{
    //// FadeOut 효과관련
    //private bool isFadeOutPlaying;
    //private float time;
    //private float fadeTime = 2f;

    //public void CabbageFadeIn() 
    //{ 
    //    //if (isFadeOutPlaying == true) return;
    //    StartCoroutine(CoFadeIn());
    //    //StartCoroutine(CoFadeOut());
    //}

    //public void CabbagePartyEnd()
    //{
    //    //if (isFadeOutPlaying == true) return;
    //    StartCoroutine(CoFadeOut());
    //}

    //IEnumerator CoFadeIn()
    //{
    //    isFadeOutPlaying = true;

    //    Image panelAlpha = GameObject.Find("CabbagePanel").GetComponent<Image>();
    //    Color fadeColor = panelAlpha.color;
    //    time = 0f;

    //    while (fadeColor.a < 0.5f)
    //    {
    //        time += Time.deltaTime / fadeTime;
    //        fadeColor.a = Mathf.Lerp(0f, 0.5f, time);
    //        panelAlpha.color = fadeColor;
    //        yield return null;
    //        //yield return new WaitForSeconds(3f);
    //    }

    //}

    //IEnumerator CoFadeOut()
    //{
    //    Image panelAlpha = GameObject.Find("CabbagePanel").GetComponent<Image>();
    //    Color fadeColor = panelAlpha.color;
    //    time = 0f;

    //    while (fadeColor.a > 0)
    //    {
    //        time += Time.deltaTime / fadeTime;
    //        fadeColor.a = Mathf.Lerp(0.5f, 0f, time);
    //        panelAlpha.color = fadeColor;
    //        yield return null;
    //    }
    //    isFadeOutPlaying = false;
    //}


    /////////////////////////
    
    private Image image;
    private float fadeTime = 2f;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void StartCabbage()
    {
        StartCoroutine(FadeInOut());
    }

    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (currentTime < 3.5f)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;           // 나누기 안해주면 무조건 1초동안 실행됨

            Color color = image.color;
            color.a = Mathf.Lerp(start, end, percent);
            image.color = color;

            yield return null;
        }
    }

    private IEnumerator FadeInOut()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(0, 0.5f));    // Fade Out
            yield return StartCoroutine(Fade(0.5f, 0));    // Fade In
            break;
        }

    }

    /*
    private void Update()
    {
        // image의 color 프로퍼티는 a 변수만 따로 set이 불가능해서 변수에 저장
        Color color = image.color;

        // FadeIn : 알파 값이 0보다 크면 알파 값 감소
        //if (color.a > 0)
        //{
        //    color.a -= Time.deltaTime / 2;
        //}

        // FadeOut : 알파 값이 1보다 작으면 알파 값 증가
        if (color.a < 0.5)
        {
            color.a += Time.deltaTime / 3;
        }

        image.color = color;
    }
    */
}
