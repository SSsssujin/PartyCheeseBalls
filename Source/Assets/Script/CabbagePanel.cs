using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CabbagePanel : MonoBehaviour
{
    //// FadeOut ȿ������
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
            percent = currentTime / fadeTime;           // ������ �����ָ� ������ 1�ʵ��� �����

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
        // image�� color ������Ƽ�� a ������ ���� set�� �Ұ����ؼ� ������ ����
        Color color = image.color;

        // FadeIn : ���� ���� 0���� ũ�� ���� �� ����
        //if (color.a > 0)
        //{
        //    color.a -= Time.deltaTime / 2;
        //}

        // FadeOut : ���� ���� 1���� ������ ���� �� ����
        if (color.a < 0.5)
        {
            color.a += Time.deltaTime / 3;
        }

        image.color = color;
    }
    */
}
