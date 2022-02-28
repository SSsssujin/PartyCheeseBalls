using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageBlink : MonoBehaviour
{
    Image fingerImg;

    private void Start()
    {
        fingerImg = GetComponent<Image>();
        StartCoroutine(EMarkerGrid());
    }

    IEnumerator EMarkerGrid()
    {
        //this.gameObject.SetActive(true);

        while (true)
        {
            fingerImg.enabled = true;
            yield return new WaitForSeconds(0.7f);

            fingerImg.enabled = false;
            yield return new WaitForSeconds(0.7f);
        }
    }
}
