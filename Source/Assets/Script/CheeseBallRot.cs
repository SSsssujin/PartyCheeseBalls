using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseBallRot : MonoBehaviour
{
    void Start()
    {
        this.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
    }
}
