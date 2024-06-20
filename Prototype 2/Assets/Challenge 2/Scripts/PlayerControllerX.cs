using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    
    private bool ready;

    private void Start() {
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && ready)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            ready = false;
            Invoke(nameof(Ready), .5f);
        }
    }

    private void Ready() {
        ready = true;
    }
}
