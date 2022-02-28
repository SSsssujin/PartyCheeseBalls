using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    private Rigidbody ssibal;
    
    //[HideInInspector] public bool isPicked;

    void Start()
    {
        ssibal = GetComponent<Rigidbody>();

        // 시작 y좌표는 무조건 13으로
        if (transform.position.y != 13f)
        {
            transform.position = new Vector3(
                transform.position.x,
                13f,
                transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.collider.name);
        //isPicked = true;

        ssibal.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

    private void OnCollisionStay(Collision collision)
    {
        ssibal.drag = 100;

        // y좌표 13 안 되게
        if (transform.position.y == 12.99f)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + 0.1f,
                transform.position.z);
        }
        if (transform.position.y == 13.01f)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y - 0.1f,
                transform.position.z);
        }
    }
}
