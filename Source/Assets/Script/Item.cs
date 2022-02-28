using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public GameObject itemPrefab;
    public GameObject effectPrefab;

    private GameObject[] items;
    public Transform[] cases;

    private float x1, y1, z1;

    public GameObject GetItem(Transform itemCase)
    {
        // 해당 조건을 만족하는 번호를 반환
        var index = System.Array.FindIndex(cases, x => x == itemCase);
        return items[index];
    }

    void Awake()
    {
        items = new GameObject[cases.Length];
        //cases = GetComponentsInChildren<Transform>();

        //for (int i = 1; i < cases.Length; i++)
        for (int i = 0; i < cases.Length; i++)
        {
            //오른쪽
            if (cases[i].rotation == Quaternion.Euler(0f, 90f, 0f))
            {
                x1 = 2.6f;
                y1 = 12f;
                z1 = 9.5f;
            }
            //왼쪽
            if (cases[i].rotation == Quaternion.Euler(0f, -90f, 0f))
            {
                x1 = -3f;
                y1 = 12f;
                z1 = -10f;
            }

            // 아이템 생성
            items[i] = Instantiate(itemPrefab, 
                new Vector3(cases[i].position.x + x1,
                cases[i].position.y + y1,
                cases[i].position.z + z1),
                Quaternion.identity); 

            #region 이펙트 생성 코드
            // 이펙트1
            Instantiate(effectPrefab,
                new Vector3(cases[i].position.x + x1,
                cases[i].position.y + y1,
                cases[i].position.z + z1),
                Quaternion.identity);
            // 이펙트2
            Instantiate(effectPrefab,
                new Vector3(cases[i].position.x + x1,
                cases[i].position.y + y1,
                cases[i].position.z + z1),
                Quaternion.identity);
            #endregion

            itemPrefab.name = $"{itemPrefab.name}";
        }
    }


}   
