using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSound : MonoBehaviour
{
    private void OnEnable()
    {
        // È¿°úÀ½ On
        gameObject.GetComponent<AudioSource>().Play();


    }
}
