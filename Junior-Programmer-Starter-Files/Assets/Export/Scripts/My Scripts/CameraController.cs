using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(0, 2)]
    public float speed;
    public Vector2 topLeftBorder, bottomRightBorder; // x and z border
    
    public static bool floating;
    public static float floatTime = 0.5f;

    private void Update() {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (Input.touchCount == 1) 
        {
            Vector3 direction = Vector3.right * SwipeDetector.direction.normalized.y - Vector3.forward * SwipeDetector.direction.normalized.x;

            direction *= speed;

            if (SwipeDetector.dragging) transform.position = new Vector3(
                Mathf.Clamp(transform.position.x + direction.x, bottomRightBorder.x, topLeftBorder.x),
                transform.position.y,
                Mathf.Clamp(transform.position.z + direction.z, bottomRightBorder.y, topLeftBorder.y));
        }
#endif
    }
}
