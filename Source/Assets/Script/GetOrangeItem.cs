using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetOrangeItem : MonoBehaviour
{
    // ������ ������ ���
    private GameObject[] items;
    private int itemNum = 0;

    // ������ �̵��� ����Ʈ
    List<GameObject> cartItems;
    List<GameObject> cartItemsCopy;
    private GameObject testItem;

    // īƮ ��ġ
    private Transform cart;
    public Transform cartPos;

    Rigidbody itemRigid;

    // �÷��̾� ����
    PlayerMovement playerInfo;

    // UI - ���θ���Ʈ
    private GameObject check;
    private Text textUI;
    private GameObject orangeUI;
    [HideInInspector] public bool isComplete = false;

    // ����� ����Ʈ
    [HideInInspector] public GameObject cabbageAttack;
    CabbagePanel cabbageEffect;
    AudioSource[] cabbageSound;
    private int minusNum = 3;
    private float cabbageTimer = 0;
    private bool cabbageStart = false;

    // ������ ȿ����
    private AudioSource itemSound;
    private AudioSource BGM;


    void Start()
    {
        playerInfo = FindObjectOfType<PlayerMovement>();

        // īƮ�� ������
        cartItems = new List<GameObject>();
        cartItemsCopy = new List<GameObject>();
        cart = GameObject.Find("Cart").transform;

        // üũ����Ʈ
        check = GameObject.Find("Check3");
        check.transform.position = new Vector3(check.transform.position.x + 6, check.transform.position.y, check.transform.position.z);
        check.SetActive(false);

        // ġ� ������ ���� ���ϱ�
        items = GameObject.FindGameObjectsWithTag("OrangeItem");
        orangeUI = GameObject.Find("OrangeUI");
        textUI = orangeUI.GetComponentInChildren<Text>();
        //textUI = GameObject.Find("OrangeText").GetComponent<Text>();
        itemNum = items.Length;

        // ����� ����Ʈ
        cabbageAttack = GameObject.Find("CabbageAttack");
        cabbageAttack.SetActive(false);
        cabbageEffect = FindObjectOfType<CabbagePanel>();
        cabbageSound = GetComponentsInChildren<AudioSource>();


        // ������ ���� �� ȿ����, BGM
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
        // UI (���� ������ ���� ǥ��)
        // UI (���� ������ ���� ǥ��)
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

        // ������ ������ ������ ��!
        if (itemNum == 0) isComplete = true;
        //if (isComplete) Debug.Log("ġ� ��!");
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
            //Debug.Log("ġ�!!");

            Item item = other.GetComponentInParent<Item>();
            testItem = item.GetItem(other.transform);

            // �÷��̾ UP/Down ������ ���� ������ ȹ��
            if (playerInfo.currDirection == PlayerMovement.Direction.UP ||
                playerInfo.currDirection == PlayerMovement.Direction.DOWN)
            {
                // ������ �������� ��ġ�ϸ�
                if (testItem.transform.position.y == 13f)
                {
                    // List �ȿ� ������ �ֱ�
                    cartItems.Add(testItem);
                    cartItemsCopy.Add(testItem);

                    // ������ ���� �÷���
                    itemSound.Play();

                    // ������ ȹ�� ��� �÷���
                    playerInfo.animator[2].SetTrigger("GetItem");

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
        if (other.CompareTag("OrangeCabbage"))
        {
           //Debug.Log("�����!!!");

           // ����ǰ� �ִ� ����Ʈ ���
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
