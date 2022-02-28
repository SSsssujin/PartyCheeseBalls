using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObject : MonoBehaviour
{
    void Start()
    {
        IndicatorManager.manager.Add(this);
    }
}
