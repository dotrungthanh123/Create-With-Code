using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public float travelledPercentage;
    public float length;

    private Transform car;
    private Collider collider;
    private bool spawned;

    private void Start() {
        car = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        collider = GetComponent<Collider>();
    }

    private void Update() {
        travelledPercentage = -Vector3.Dot(car.position - transform.position, transform.right) / length * 2;

        if (travelledPercentage >= 0.8 && !spawned) {
            spawned = true;
            GameObject newRoad = Instantiate(gameObject, transform.position + Vector3.forward * length, transform.rotation);
        }
    }
}
