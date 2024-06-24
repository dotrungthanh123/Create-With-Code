using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Transform target;
    public Camera cam;
    public float yOffset;
    public Image foreground;

    private void Start() {
        cam = Camera.main;
    }

    private void Update() {
        if (target) transform.position = cam.ViewportToScreenPoint(cam.WorldToViewportPoint(target.position + yOffset * Vector3.up));
    }

    public void UpdateHp(float percent) {
        foreground.fillAmount = percent;
    }
}