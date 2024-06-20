using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed;
    private PlayerController playerController;
    private Rigidbody playerRb;
    private Vector3 force;

    private void Start() {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerRb = playerController.GetComponent<Rigidbody>();
    }

    private void Update() {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);

        Vector3 playerVelocity = playerRb.velocity;
        Vector3 desiredVelocity = Vector3.Dot(transform.forward, playerVelocity) * transform.forward;
        force = desiredVelocity - playerVelocity;

    }

    private void FixedUpdate() {
        playerRb.AddForce(force, ForceMode.VelocityChange);
    }
}
