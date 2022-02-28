using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetOrangeItem : MonoBehaviour
{
    // 생성된 아이템 목록
    private GameObject[] items;
    private int itemNum = 0;

    // 아이템 이동용 리스트
    List<GameObject> cartItems;
    List<GameObject> cartItemsCopy;
    private GameObject testItem;

    // 카트 위치
    private Transform cart;
    public Transform cartPos;

    Rigidbody itemRigid;

    // 플레이어 정보
    PlayerMovement playerInfo;

    // UI - 쇼핑리스트
    private GameObject check;
    private Text textUI;
    private GameObject orangeUI;
    [HideInInspector] public bool isComplete = false;

    // 양배추 이펙트
    [HideInInspector] public GameObject cabbageAttack;
    CabbagePanel cabbageEffect;
    AudioSource[] cabbageSound;
    private int minusNum = 3;
    private float cabbageTimer = 0;
    private bool cabbageStart = false;

    // 아이템 효과음
    private AudioSource itemSound;
    private AudioSource BGM;


    void Start()
    {
        playerInfo = FindObjectOfType<PlayerMovement>();

        // 카트에 아이템
        cartItems = new List<GameObject>();
        cartItemsCopy = new List<GameObject>();
        cart = GameObject.Find("Cart").transform;

        // 체크리스트
        check = GameObject.Find("Check3");
        check.transform.position = new Vector3(check.transform.position.x + 6, check.transform.position.y, check.transform.position.z);
        check.SetActive(false);

        // 치즈볼 아이템 개수 구하기
        items = GameObject.FindGameObjectsWithTag("OrangeItem");
        orangeUI = GameObject.Find("OrangeUI");
        textUI = orangeUI.GetComponentInChildren<Text>();
        //textUI = GameObject.Find("OrangeText").GetComponent<Text>();
        itemNum = items.Length;

        // 양배추 이펙트
        cabbageAttack = GameObject.Find("CabbageAttack");
        cabbageAttack.SetActive(false);
        cabbageEffect = FindObjectOfType<CabbagePanel>();
        cabbageSound = GetComponentsInChildren<AudioSource>();


        // 아이템 담을 때 효과음, BGM
        itemSound = GetComponent<AudioSource>();
        BGM = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (cabbageStart)
            cabbageTimer += Time.deltaTime;
        if (cabbageTimer > 4)
        {
            BGM.Play();
            cabbageStart = false;
            cabbageTimer = 0;
        }
        //Debug.Log(check.name);
        // UI (남은 아이템 개수 표시)
        // UI (남은 아이템 개수 표시)
        if (items.Length == 0)
        {
            itemNum = 0;
            orangeUI.SetActive(false);
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

        // 오렌지 아이템 모으기 완!
        if (itemNum == 0) isComplete = true;
        //if (isComplete) Debug.Log("치즈볼 끝!");
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
        if (other.CompareTag("Orange"))
        {
            //Debug.Log("치즈볼!!");

            Item item = other.GetComponentInParent<Item>();
            testItem = item.GetItem(other.transform);

            // 플레이어가 UP/Down 상태일 때만 아이템 획득
            if (playerInfo.currDirection == PlayerMovement.Direction.UP ||
                playerInfo.currDirection == PlayerMovement.Direction.DOWN)
            {
                // 아이템 포지션이 일치하면
                if (testItem.transform.position.y == 13f)
                {
                    // List 안에 아이템 넣기
                    cartItems.Add(testItem);
                    cartItemsCopy.Add(testItem);

                    // 아이템 사운드 플레이
                    itemSound.Play();

                    // 아이템 획득 모션 플레이
                    playerInfo.animator[2].SetTrigger("GetItem");

                    // 진열장 색깔 하얀색으로
                    MeshRenderer[] caseColors = other.GetComponentsInChildren<MeshRenderer>();

                    for (int i = 0; i < caseColors.Length; i++)
                    {
                        caseColors[i].material.color = Color.white;
                    }
                }
            }
        }

        // 양배추 획득 시
        if (other.CompareTag("OrangeCabbage"))
        {
           //Debug.Log("양배추!!!");

           // 실행되고 있는 이펙트 취소
           cabbageAttack.SetActive(false);

           cabbageEffect.StartCabbage();
           cabbageAttack.SetActive(true);
           itemSound.Play();
           cabbageSound[1].Play();

           playerInfo.moveNum -= minusNum;

            BGM.Pause();
            cabbageStart = true;
        }
    }
}
