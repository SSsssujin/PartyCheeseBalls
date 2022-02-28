using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPurpleItem : MonoBehaviour
{
    // ������ ������ ���
    private GameObject[] items;
    private int itemNum = 0;

    PlayerMovement playerInfo;

    // ������ �̵��� ����Ʈ
    List<GameObject> cartItems;
    List<GameObject> cartItemsCopy;
    private GameObject testItem;

    // īƮ ��ġ
    public Transform cartPos;
    private Transform cart;

    Rigidbody itemRigid;

    // UI (���θ���Ʈ)
    private GameObject check;
    private Text textUI;
    private GameObject purpleUI;
    [HideInInspector] public bool isComplete = false;

    // ����� ���� ȿ��
    CabbagePanel cabbageEffect;
    GetOrangeItem getOrangeItem;
    AudioSource[] cabbageSound;
    //GameObject cabbageAttack;
    //CabbagePanel cabbageEffect;
    //AudioSource[] cabbageSound;
    private int minusNum = 3;
    private float cabbageTimer = 0;
    private bool cabbageStart = false;

    // ������ ȿ����
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

        // ����� ������ ���� ���ϱ�
        items = GameObject.FindGameObjectsWithTag("PurpleItem");
        itemNum = items.Length;

        cartItems = new List<GameObject>();
        cartItemsCopy = new List<GameObject>();

        // ������ ���� �� ȿ����
        itemSound = GetComponent<AudioSource>();

        // ����� ����Ʈ
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

        // UI (���� ������ ���� ǥ��)
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

        // ����� ������ ������ ��!
        if (itemNum == 0) isComplete = true;
        //if (isComplete) Debug.Log("����� ��!");
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
            //Debug.Log("�����!!");

            var item = other.GetComponentInParent<Item>();
            testItem = item.GetItem(other.transform);

            // �÷��̾ UP/Down ������ ���� ������ ȹ��
            if (playerInfo.currDirection == PlayerMovement.Direction.UP ||
                playerInfo.currDirection == PlayerMovement.Direction.DOWN)
            {
                if (testItem.transform.position.y == 13f)
                {
                    cartItems.Add(testItem);
                    cartItemsCopy.Add(testItem);

                    itemSound.Play();

                    playerInfo.animator[1].SetTrigger("GetItem");

                    // ������ ���� �Ͼ������
                    MeshRenderer[] caseColors = other.GetComponentsInChildren<MeshRenderer>();

                    for (int i = 0; i < caseColors.Length; i++)
                    {
                        caseColors[i].material.color = Color.white;
                    }
                }
            }
        }

        // ����� ȹ�� ��
        //if (other.tag == "PurpleCabbage")
        if (other.CompareTag("PurpleCabbage"))
        {
            //Debug.Log("�����!!!");

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
