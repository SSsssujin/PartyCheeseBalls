using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetWhiteItem : MonoBehaviour
{
    PlayerMovement playerInfo;

    // ������ ������ ���
    private GameObject[] items;
    private int itemNum = 0;

    // ������ �̵��� ����Ʈ
    List<GameObject> cartItems;
    List<GameObject> cartItemsCopy;
    private GameObject testItem;

    Rigidbody itemRigid;

    // īƮ ��ġ
    public Transform cartPos;
    private Transform cart;

    // UI (���θ���Ʈ)
    private GameObject check;
    private Text textUI;
    private GameObject whiteUI;
    [HideInInspector] public bool isComplete = false;

    // ������ ȿ����
    private AudioSource itemSound;

    void Start()
    {
        playerInfo = FindObjectOfType<PlayerMovement>();

        // UI�� üũ ã�Ƽ� ����
        check = GameObject.Find("Check1");
        check.transform.position = new Vector3(check.transform.position.x + 6, check.transform.position.y, check.transform.position.z);
        check.SetActive(false);

        // �� �̸��� �ش��ϴ� ������Ʈ ã��
        cart = GameObject.Find("Cart").transform;
        whiteUI = GameObject.Find("WhiteUI");
        textUI = whiteUI.GetComponentInChildren<Text>();
        //textUI = GameObject.Find("WhiteText").GetComponent<Text>();

        // ġ� ������ ���� ���ϱ�
        items = GameObject.FindGameObjectsWithTag("WhiteItem");
        itemNum = items.Length;

        cartItems = new List<GameObject>();
        cartItemsCopy = new List<GameObject>();

        itemSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        // UI (���� ������ ���� ǥ��)
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

        // ���� ������ ������ ��!
        if (itemNum == 0) isComplete = true;
        //if (isComplete) Debug.Log("���� ��!");
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
            //Debug.Log("����!!");

            var item = other.GetComponentInParent<Item>();
            testItem = item.GetItem(other.transform);

            // �÷��̾ UP/Down ������ ���� ������ ȹ��

            if (playerInfo.currDirection == PlayerMovement.Direction.UP ||
                playerInfo.currDirection == PlayerMovement.Direction.DOWN)
            {
                // ���� ���� �����ֱ�!!
                if (testItem.transform.position.y == 13f)
                {
                    cartItems.Add(testItem);
                    cartItemsCopy.Add(testItem);

                    itemSound.Play();

                    playerInfo.animator[0].SetTrigger("GetItem");

                    // ������ ȹ�� �� ������ ���� �Ͼ������
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
