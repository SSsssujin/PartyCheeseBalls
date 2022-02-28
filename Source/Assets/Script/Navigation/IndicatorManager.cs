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
         // indicator 위치에 따른 피벗 변경
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
        // 월드 좌표계 -> 뷰포트 좌표계
        Vector3 point = Camera.main.WorldToViewportPoint(target.transform.position);
        
        // 타겟과 카메라 사이의 거리 구하기
        Vector3 dirToTarget = (target.transform.position - Camera.main.transform.position).normalized;
        
        // 카메라 앞
        if (Vector3.Dot(Camera.main.transform.forward, dirToTarget) > 0)
        {
            //Debug.Log("앞");

          point.x = Mathf.Clamp01(point.x);           // 최소값 0, 최대값 1 사이에서 clamp
          point.y = Mathf.Clamp01(point.y);           // 최소값 0, 최대값 1 사이에서 clamp
        }
        // 카메라 뒤
        else
        {
            //Debug.Log("뒤");
            
            point.x = 1 - Mathf.Clamp01(point.x);        
            point.y = 1 - Mathf.Clamp01(point.y);        
        }

        Canvas canvas = container.GetComponentInParent<Canvas>();
        RectTransform canvasRectTr = canvas.GetComponent<RectTransform>();
        point *= canvasRectTr.sizeDelta;          // 캔버스의 Width, Height

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
        indicatorRectTr.anchorMin = Vector2.zero;  //건들지말기
        indicatorRectTr.anchorMax = Vector2.zero;  //건들지말기
        indicatorRectTr.anchoredPosition = GetCanvasPosition(target);

        indicators.Add(target, indicatorRectTr);
    }

}
