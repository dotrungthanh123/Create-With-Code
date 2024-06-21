using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 500;
    private GameObject focalPoint;
    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup
    private float speedBoost = 2;
    private bool speeding;
    private ParticleSystem boost;
    private float boostingTime;
    private float oneEmitTime; // time for an emission of 1 particle
    private RotateCameraX camera;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 5;
    public int emitPerSec;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        boost = FindObjectOfType<ParticleSystem>();
        boostingTime = 0;
        camera = FindObjectOfType<RotateCameraX>();
    }

    void Update()
    {
        oneEmitTime = 1.0f / emitPerSec; // edit in editor need time update

        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        speeding = Input.GetKey(KeyCode.Space);
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime * (speeding ? speedBoost : 1));

        boost.transform.position = transform.position + new Vector3(0, -0.5f, -0.25f);
        boost.transform.forward = -camera.transform.forward;
        if (speeding)
        {
            boostingTime += Time.deltaTime;
            int emitNum = Mathf.FloorToInt(boostingTime / oneEmitTime);
            if (emitNum > 0)
            {
                boost.Emit(emitNum);
                boostingTime -= emitNum * oneEmitTime;
            }
        }
        else
        {
            boostingTime = 0;
        }

        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = transform.InverseTransformPoint(other.gameObject.transform.position).normalized;

            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }


        }
    }



}
