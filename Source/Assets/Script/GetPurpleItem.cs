using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPurpleItem : MonoBehaviour
{
    // 생성된 아이템 목록
    private GameObject[] items;
    private int itemNum = 0;

    PlayerMovement playerInfo;

    // 아이템 이동용 리스트
    List<GameObject> cartItems;
    List<GameObject> cartItemsCopy;
    private GameObject testItem;

    // 카트 위치
    public Transform cartPos;
    private Transform cart;

    Rigidbody itemRigid;

    // UI (쇼핑리스트)
    private GameObject check;
    private Text textUI;
    private GameObject purpleUI;
    [HideInInspector] public bool isComplete = false;

    // 양배추 관련 효과
    CabbagePanel cabbageEffect;
    GetOrangeItem getOrangeItem;
    AudioSource[] cabbageSound;
    //GameObject cabbageAttack;
    //CabbagePanel cabbageEffect;
    //AudioSource[] cabbageSound;
    private int minusNum = 3;
    private float cabbageTimer = 0;
    private bool cabbageStart = false;

    // 아이템 효과음
    private AudioSource itemSound;
    private AudioSource BGM;


    void Start()
    {
        playerInfo = FindObjectOfType<PlayerMovement>();

        //isComplete = true;
        check = GameObject.Find("Check2");
        check.transform.position = new Vector3(check.transform.position.x + 6, check.transform.position.y, check.transform.position.z);
        check.SetActive(false);

        cart = GameObject.Find("Cart").transform;
        purpleUI = GameObject.Find("PurpleUI");
        textUI = purpleUI.GetComponentInChildren<Text>();
        //textUI = GameObject.Find("PurpleText").GetComponent<Text>();

        // 보라색 아이템 개수 구하기
        items = GameObject.FindGameObjectsWithTag("PurpleItem");
        itemNum = items.Length;

        cartItems = new List<GameObject>();
        cartItemsCopy = new List<GameObject>();

        // 아이템 담을 때 효과음
        itemSound = GetComponent<AudioSource>();

        // 양배추 이펙트
        cabbageEffect = FindObjectOfType<CabbagePanel>();
        getOrangeItem = FindObjectOfType<GetOrangeItem>();
        cabbageSound = GetComponentsInChildren<AudioSource>();
        //cabbageAttack = GameObject.Find("CabbageAttack");
        ////cabbageAttack.SetActive(false);
        //cabbageEffect = FindObjectOfType<CabbagePanel>();
        //cabbageSound = GetComponentsInChildren<AudioSource>();
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

        // UI (남은 아이템 개수 표시)
        if (items.Length == 0)
        {
            itemNum = 0;
            purpleUI.SetActive(false);
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

        // 보라색 아이템 모으기 완!
        if (itemNum == 0) isComplete = true;
        //if (isComplete) Debug.Log("보라색 끝!");
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
        //if (other.tag == "Purple")
        if (other.CompareTag("Purple"))
        {
            //Debug.Log("보라색!!");

            var item = other.GetComponentInParent<Item>();
            testItem = item.GetItem(other.transform);

            // 플레이어가 UP/Down 상태일 때만 아이템 획득
            if (playerInfo.currDirection == PlayerMovement.Direction.UP ||
                playerInfo.currDirection == PlayerMovement.Direction.DOWN)
            {
                if (testItem.transform.position.y == 13f)
                {
                    cartItems.Add(testItem);
                    cartItemsCopy.Add(testItem);

                    itemSound.Play();

                    playerInfo.animator[1].SetTrigger("GetItem");

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
        //if (other.tag == "PurpleCabbage")
        if (other.CompareTag("PurpleCabbage"))
        {
            //Debug.Log("양배추!!!");

            getOrangeItem.cabbageAttack.SetActive(false);

            cabbageEffect.StartCabbage();
            getOrangeItem.cabbageAttack.SetActive(true);
            itemSound.Play();
            cabbageSound[1].Play();

            playerInfo.moveNum -= minusNum;
            
            BGM.Pause();
            cabbageStart = true;
        }
    }
}
