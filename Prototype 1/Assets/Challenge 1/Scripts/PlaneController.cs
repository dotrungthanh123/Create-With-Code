using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    private int verticalInput;
    private Rigidbody rb;
    private float time;
    public float duration;
    private Quaternion rotationOnKeyUp;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        time = 0;
        rotationOnKeyUp = Quaternion.identity;
    }

    private void Update() {
        // get the user's vertical input
        verticalInput = (int) Input.GetAxisRaw("Vertical");

        if (verticalInput == 0 && time == 0) {
            rotationOnKeyUp = transform.rotation;
        }

        time += Time.deltaTime;
        time *= verticalInput == 0 ? 1 : 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // move the plane forward at a constant rate
        rb.MovePosition(transform.position + transform.forward * speed);

        // tilt the plane up/down based on up/down arrow keys
        rb.MoveRotation(transform.rotation * Quaternion.Euler(transform.right * rotationSpeed * Time.fixedDeltaTime * -verticalInput));
        
        if (verticalInput == 0) Rotate();
    }

    private void Rotate() {
        Quaternion rot = Quaternion.Lerp(rotationOnKeyUp, Quaternion.identity, time / duration);
        rb.MoveRotation(rot);
    }

    
}
