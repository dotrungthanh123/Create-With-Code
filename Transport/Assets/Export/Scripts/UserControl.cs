using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handle all the control code, so detecting when the users click on a unit or building and selecting those
/// If a unit is selected it will give the order to go to the clicked point or building when right clicking.
/// </summary>
public class UserControl : MonoBehaviour
{
    public Camera GameCamera;
    public float PanSpeed = 10.0f;
    public GameObject Marker;

    private Unit m_Selected = null;
    private float touchTime;

    private void Start()
    {
        Marker.SetActive(false);
    }

    private void Update()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        GameCamera.transform.position = GameCamera.transform.position + new Vector3(move.y, 0, -move.x) * PanSpeed * Time.deltaTime;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            HandleSelection();
        }
        else if (m_Selected != null && Input.GetMouseButtonDown(1))
        {//right click give order to the unit
            HandleAction();
        }

        // if (Input.GetMouseButtonDown(0)) {
        //     touchTime = 0;
        // } else if (Input.GetMouseButton(0)) {
        //     touchTime += Time.deltaTime;
        //     if (touchTime > 1) HandleAction();
        // } else if (Input.GetMouseButtonUp(0)) {
        //     if (touchTime < 0.12f) HandleSelection();
        // }
#elif UNITY_ANDROID
        if (Input.touchCount == 1) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                touchTime = 0;
            } else if (touch.phase == TouchPhase.Ended) {
                if (touchTime < 0.12f) {
                    HandleSelection();
                }
            }
            touchTime += Time.deltaTime;
            if (touchTime > 1) HandleAction(); 
        }
#endif
        MarkerHandling();
    }

    // Handle displaying the marker above the unit that is currently selected (or hiding it if no unit is selected)
    void MarkerHandling()
    {
        if (m_Selected == null && Marker.activeInHierarchy)
        {
            Marker.SetActive(false);
            Marker.transform.SetParent(null);
        }
        else if (m_Selected != null && Marker.transform.parent != m_Selected.transform)
        {
            Marker.SetActive(true);
            Marker.transform.SetParent(m_Selected.transform, false);
            Marker.transform.localPosition = Vector3.zero;
        }
    }

    public void HandleSelection()
    {
#if UNITY_EDITOR
            var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
#elif UNITY_ANDROID
            var ray = GameCamera.ScreenPointToRay(Input.GetTouch(0).position);
#endif
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // the collider could be children of the unit, so we make sure to check in the parent
            var unit = hit.collider.GetComponentInParent<Unit>();
            m_Selected = unit;

            // check if the hit object have a IUIInfoContent to display in the UI
            // if there is none, this will be null, so this will hid the panel if it was displayed
            var uiInfo = hit.collider.GetComponentInParent<UIMainScene.IUIInfoContent>();
            UIMainScene.Instance.SetNewInfoContent(uiInfo);
        }
    }

    public void HandleAction()
    {
#if UNITY_EDITOR
            var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
#elif UNITY_ANDROID
            var ray = GameCamera.ScreenPointToRay(Input.GetTouch(0).position);
#endif
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var building = hit.collider.GetComponentInParent<Building>();

            if (building != null)
            {
                m_Selected.GoTo(building);
            }
            else
            {
                m_Selected.GoTo(hit.point);
            }
        }
    }
}