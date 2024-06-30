using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeDetector : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public static Vector2 direction;
    public static bool dragging;

    Vector2 prevPos;

    public void OnBeginDrag(PointerEventData eventData)
    {
        prevPos = eventData.position;
        dragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        direction = prevPos - eventData.position;
        prevPos = eventData.position;
        dragging = true;
    }

    private void Update() {
        dragging = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
        direction = Vector3.zero;
    }

    IEnumerator Floating()
    {
        while (!dragging)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            direction = Vector3.Lerp(direction, Vector3.zero, Time.deltaTime * 7.5f);
        }
    }
}
