using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkleCollision : MonoBehaviour
{
    private GameObject effectPrefab;

    private void Awake()
    {
        effectPrefab = this.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.name);
        //Debug.Log(effectPrefab.name);

        Destroy(effectPrefab);
    }
}
