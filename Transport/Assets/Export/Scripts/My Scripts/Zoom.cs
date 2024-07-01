using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public int zoomSpeed;
    public Vector2 zoomRange;

    Camera cam;
    float prevTouchDistance;
    float zoomModifier;

    private void Start() {
        cam = Camera.main;
    }

    private void Update() {
        if (Input.touchCount == 2) {
			Touch firstTouch = Input.GetTouch (0);
			Touch secondTouch = Input.GetTouch (1);

            if (prevTouchDistance != 0) {
                float currentDistance = Vector3.Distance(firstTouch.position, secondTouch.position);
                zoomModifier = currentDistance - prevTouchDistance;
            } else {
                zoomModifier = 0;            
            }

            prevTouchDistance = Vector3.Distance(firstTouch.position, secondTouch.position);
		} else {
            prevTouchDistance = 0;
        }

        float newFieldOfView = cam.fieldOfView - zoomModifier * zoomSpeed * Time.deltaTime;

		cam.fieldOfView = Mathf.Clamp (newFieldOfView, zoomRange.x, zoomRange.y);
    }
}
