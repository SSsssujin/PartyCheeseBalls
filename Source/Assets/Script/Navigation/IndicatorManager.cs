using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorManager : MonoBehaviour
{
    public static IndicatorManager manager;

    public GameObject prefab;
    public RectTransform container;

    [HideInInspector]
    public Dictionary<TrackObject, RectTransform> indicators =
        new Dictionary<TrackObject, RectTransform>();

    GameObject indicator;
    RectTransform indicatorRectTr;

    private void Awake()
    {
         manager = this;
    }

    private void LateUpdate()
    {

       if (indicator != null)
       {
         // indicator ��ġ�� ���� �ǹ� ����
           Vector2 point = Camera.main.ScreenToViewportPoint(indicator.transform.position);
       
           if (point.x <= 0f) indicatorRectTr.pivot = new Vector2(0f, indicatorRectTr.pivot.y);
           if (point.x >= 1f) indicatorRectTr.pivot = new Vector2(1f, indicatorRectTr.pivot.y);
           if (point.y <= 0f) indicatorRectTr.pivot = new Vector2(indicatorRectTr.pivot.x, 0f);
           if (point.y >= 1f) indicatorRectTr.pivot = new Vector2(indicatorRectTr.pivot.x, 1f);
       
       
           foreach (var pair in indicators)
           {
               pair.Value.anchoredPosition = GetCanvasPosition(pair.Key);
           }
       }
       else return;
    }

    private Vector2 GetCanvasPosition(TrackObject target)
    {
        // ���� ��ǥ�� -> ����Ʈ ��ǥ��
        Vector3 point = Camera.main.WorldToViewportPoint(target.transform.position);
        
        // Ÿ�ٰ� ī�޶� ������ �Ÿ� ���ϱ�
        Vector3 dirToTarget = (target.transform.position - Camera.main.transform.position).normalized;
        
        // ī�޶� ��
        if (Vector3.Dot(Camera.main.transform.forward, dirToTarget) > 0)
        {
            //Debug.Log("��");

          point.x = Mathf.Clamp01(point.x);           // �ּҰ� 0, �ִ밪 1 ���̿��� clamp
          point.y = Mathf.Clamp01(point.y);           // �ּҰ� 0, �ִ밪 1 ���̿��� clamp
        }
        // ī�޶� ��
        else
        {
            //Debug.Log("��");
            
            point.x = 1 - Mathf.Clamp01(point.x);        
            point.y = 1 - Mathf.Clamp01(point.y);        
        }

        Canvas canvas = container.GetComponentInParent<Canvas>();
        RectTransform canvasRectTr = canvas.GetComponent<RectTransform>();
        point *= canvasRectTr.sizeDelta;          // ĵ������ Width, Height

        return point;
    }

    public void Add(TrackObject target)
    {
        if (indicators.ContainsKey(target))
            return;

        //GameObject indicator = Instantiate(prefab, container);
        //RectTransform indicatorRectTr = indicator.GetComponent<RectTransform>();
        indicator = Instantiate(prefab, container);
        indicatorRectTr = indicator.GetComponent<RectTransform>();

        //indicatorRectTr.pivot = new Vector2(0.5f, 0.5f);
        indicatorRectTr.anchorMin = Vector2.zero;  //�ǵ�������
        indicatorRectTr.anchorMax = Vector2.zero;  //�ǵ�������
        indicatorRectTr.anchoredPosition = GetCanvasPosition(target);

        indicators.Add(target, indicatorRectTr);
    }

}
