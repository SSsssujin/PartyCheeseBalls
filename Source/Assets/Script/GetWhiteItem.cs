using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetWhiteItem : MonoBehaviour
{
    PlayerMovement playerInfo;

    // 생성된 아이템 목록
    private GameObject[] items;
    private int itemNum = 0;

    // 아이템 이동용 리스트
    List<GameObject> cartItems;
    List<GameObject> cartItemsCopy;
    private GameObject testItem;

    Rigidbody itemRigid;

    // 카트 위치
    public Transform cartPos;
    private Transform cart;

    // UI (쇼핑리스트)
    private GameObject check;
    private Text textUI;
    private GameObject whiteUI;
    [HideInInspector] public bool isComplete = false;

    // 아이템 효과음
    private AudioSource itemSound;

    void Start()
    {
        playerInfo = FindObjectOfType<PlayerMovement>();

        // UI의 체크 찾아서 해제
        check = GameObject.Find("Check1");
        check.transform.position = new Vector3(check.transform.position.x + 6, check.transform.position.y, check.transform.position.z);
        check.SetActive(false);

        // 각 이름에 해당하는 오브젝트 찾기
        cart = GameObject.Find("Cart").transform;
        whiteUI = GameObject.Find("WhiteUI");
        textUI = whiteUI.GetComponentInChildren<Text>();
        //textUI = GameObject.Find("WhiteText").GetComponent<Text>();

        // 치즈볼 아이템 개수 구하기
        items = GameObject.FindGameObjectsWithTag("WhiteItem");
        itemNum = items.Length;

        cartItems = new List<GameObject>();
        cartItemsCopy = new List<GameObject>();

        itemSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        // UI (남은 아이템 개수 표시)
        if (items.Length == 0)
        {
            itemNum = 0;
            whiteUI.SetActive(false);
        }
        else
        {
            itemNum = items.Length - cartItems.Count;

            if (itemNum < 0) itemNum = 0;
            textUI.text = " x " + itemNum;

            if (itemNum == 0) check.SetActive(true);

            if (testItem == null)
            {
                return;
            }
            else
            {
                GetItemInCart();
            }
        }

        // 우유 아이템 모으기 완!
        if (itemNum == 0) isComplete = true;
        //if (isComplete) Debug.Log("우유 끝!");
    }

    float timer;

    public void GetItemInCart()
    {
        for (int i = cartItemsCopy.Count - 1; i >= 0; i--)
        {
            if (Vector3.Distance(cartItemsCopy[i].transform.position, cartPos.position) < 1.5f)
            {
                itemRigid = cartItemsCopy[i].GetComponent<Rigidbody>();
                itemRigid.isKinematic = false;

                cartItemsCopy[i].transform.SetParent(cart);
                cartItemsCopy.RemoveAt(i);

                timer = 0;
            }
            else
            {
                //float timer = 0;
                timer += Time.deltaTime;

                if (timer >= 0.3f)
                {
                    //itemSound.Play();

                    cartItemsCopy[i].transform.position = Vector3.Lerp(
                        cartItemsCopy[i].transform.position, cartPos.position, Time.deltaTime * 5f);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);

        if (other.tag == "White")
        {
            //Debug.Log("우유!!");

            var item = other.GetComponentInParent<Item>();
            testItem = item.GetItem(other.transform);

            // 플레이어가 UP/Down 상태일 때만 아이템 획득

            if (playerInfo.currDirection == PlayerMovement.Direction.UP ||
                playerInfo.currDirection == PlayerMovement.Direction.DOWN)
            {
                // 높이 조절 잘해주기!!
                if (testItem.transform.position.y == 13f)
                {
                    cartItems.Add(testItem);
                    cartItemsCopy.Add(testItem);

                    itemSound.Play();

                    playerInfo.animator[0].SetTrigger("GetItem");

                    // 아이템 획득 시 진열장 색깔 하얀색으로
                    MeshRenderer[] caseColors = other.GetComponentsInChildren<MeshRenderer>();

                    for (int i = 0; i < caseColors.Length; i++)
                    {
                        caseColors[i].material.color = Color.white;
                    }
                }
            }
        }
    }
}
