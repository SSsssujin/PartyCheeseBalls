using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    // 타겟 설정
    private Transform  target;  // 타겟 찾기
    private float speed = 5f;   // 타겟 쫓는 속도

    // 탑뷰 조절
    private Vector3 startPos = new Vector3(0.5f, 160f, -57f);
    public Vector3 startRot = new Vector3(50f, 0f, 0f);

    // 백뷰 조절
    public float distance = 10f;    // 카메라, 플레이어 간 거리
    public float rotation = 4f;     // 카메라 각도 - 수치 높일수록 위쪽 봄
    public float height = 6f;       // 카메라 높이
    
    [HideInInspector] public bool isStarted = false;

    // 시작 시 버튼 위치 조절
    private GameObject buttons;
    RectTransform buttonRectTrasform;

    // 힌트 관련
    [HideInInspector] public int hintNum = 2;
    private GameObject hint;
    private GameObject noHint;
    [HideInInspector] private bool isNowHint = false;


    void Start()
    {
        target = GameObject.Find("Players").transform;
        buttons = GameObject.Find("Buttons");

        // 버튼 포지션
        buttonRectTrasform = buttons.GetComponent<RectTransform>();

        // Hint
        hint = GameObject.Find("Hint");
        // No Hint
        noHint = GameObject.Find("No Hint");
        noHint.SetActive(false);
    }

    void Update()
    {
        // 카메라 위치 조정
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

        // 카메라 시점 변경
#if UNITY_ANDROID
        //터치구현
        if (Input.touchCount > 0) isStarted = true;
#endif
        if (Input.GetMouseButtonDown(0)) isStarted = true;

        //힌트 남은 개수 표시
        Text chanceNum = hint.GetComponentInChildren<Text>();
        chanceNum.text = hintNum.ToString();
    }

    private void LookBack()
    {
        Vector3 temp = target.position - target.forward.normalized * distance;
        temp = new Vector3(temp.x, temp.y + height, temp.z);
        transform.position = Vector3.Lerp(transform.position, temp, Time.deltaTime * speed); //선형보간

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

    #region 시작 시 버튼
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

        // 힌트 이용권 있을 때.
        if (hintNum > 0)
        {
            hintNum -= 1;

            isNowHint = true;
            //hint.GetComponentsInChildren<Image>()[1].enabled = true;
            GameObject.Find("OK Button").GetComponent<Image>().enabled = true;
        }
        // 힌트 이용권 없을 때.
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




