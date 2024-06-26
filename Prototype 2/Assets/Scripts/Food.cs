using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int speed = 40;

    private void Update() {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
