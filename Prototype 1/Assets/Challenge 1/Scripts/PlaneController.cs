using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    private float verticalInput;

    // Update is called once per frame
    void FixedUpdate()
    {
        // get the user's vertical input
        verticalInput = Input.GetAxis("Vertical");

        // move the plane forward at a constant rate
        transform.Translate(transform.forward * speed);

        // tilt the plane up/down based on up/down arrow keys
        transform.Rotate(transform.right * rotationSpeed * Time.deltaTime * -verticalInput);
    }
}
