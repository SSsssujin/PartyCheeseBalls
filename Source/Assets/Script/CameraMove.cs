using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    // Ÿ�� ����
    private Transform  target;  // Ÿ�� ã��
    private float speed = 5f;   // Ÿ�� �Ѵ� �ӵ�

    // ž�� ����
    private Vector3 startPos = new Vector3(0.5f, 160f, -57f);
    public Vector3 startRot = new Vector3(50f, 0f, 0f);

    // ��� ����
    public float distance = 10f;    // ī�޶�, �÷��̾� �� �Ÿ�
    public float rotation = 4f;     // ī�޶� ���� - ��ġ ���ϼ��� ���� ��
    public float height = 6f;       // ī�޶� ����
    
    [HideInInspector] public bool isStarted = false;

    // ���� �� ��ư ��ġ ����
    private GameObject buttons;
    RectTransform buttonRectTrasform;

    // ��Ʈ ����
    [HideInInspector] public int hintNum = 2;
    private GameObject hint;
    private GameObject noHint;
    [HideInInspector] private bool isNowHint = false;


    void Start()
    {
        target = GameObject.Find("Players").transform;
        buttons = GameObject.Find("Buttons");

        // ��ư ������
        buttonRectTrasform = buttons.GetComponent<RectTransform>();

        // Hint
        hint = GameObject.Find("Hint");
        // No Hint
        noHint = GameObject.Find("No Hint");
        noHint.SetActive(false);
    }

    void Update()
    {
        // ī�޶� ��ġ ����
        if (!isStarted || isNowHint)
        {
            LookTop();
            HideButton();
        }
        if (isStarted && !isNowHint)
        {
            LookBack();
            SeeButton();
        }

        // ī�޶� ���� ����
#if UNITY_ANDROID
        //��ġ����
        if (Input.touchCount > 0) isStarted = true;
#endif
        if (Input.GetMouseButtonDown(0)) isStarted = true;

        //��Ʈ ���� ���� ǥ��
        Text chanceNum = hint.GetComponentInChildren<Text>();
        chanceNum.text = hintNum.ToString();
    }

    private void LookBack()
    {
        Vector3 temp = target.position - target.forward.normalized * distance;
        temp = new Vector3(temp.x, temp.y + height, temp.z);
        transform.position = Vector3.Lerp(transform.position, temp, Time.deltaTime * speed); //��������

        Vector3 forward = temp - target.position;
        var toRot = Quaternion.LookRotation(forward.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRot, Time.deltaTime * speed * 0.5f);

        Vector3 test = new Vector3(0f, rotation, 0f);
        transform.LookAt(target.position + test);
    }

    private void LookTop()
    {
        transform.position = startPos;
        transform.rotation = Quaternion.Euler(startRot);
    }

    #region ���� �� ��ư
    private void HideButton()
    {
        buttonRectTrasform.anchoredPosition = new Vector2(0f, -1500f);
    }
    private void SeeButton()
    {
        buttonRectTrasform.anchoredPosition = Vector2.Lerp(buttonRectTrasform.anchoredPosition, 
            new Vector2(0f, -300f), Time.deltaTime * 3f);
    }
    #endregion

    public void HintPressed()
    {
        if (isNowHint) return;

        // ��Ʈ �̿�� ���� ��.
        if (hintNum > 0)
        {
            hintNum -= 1;

            isNowHint = true;
            //hint.GetComponentsInChildren<Image>()[1].enabled = true;
            GameObject.Find("OK Button").GetComponent<Image>().enabled = true;
        }
        // ��Ʈ �̿�� ���� ��.
        else
        {
            noHint.SetActive(true);
        }
    }

    public void CloseHint()
    {
        isNowHint = false;
        //hint.GetComponentsInChildren<Image>()[1].enabled = false;
        GameObject.Find("OK Button").GetComponent<Image>().enabled = false;
    }

    public void CloseNoHint()
    {
        noHint.SetActive(false);
    }
}




