using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public HealthBar healthBar;

    private float spawnRangeX = 10;
    private float spawnPosZ = 20;
    private float startDelay = 2;
    private float spawnInterval = 1.5f;
    private Transform canvas;

    private void Start() {
        canvas = GameObject.Find("Canvas").GetComponent<Transform>();
        InvokeRepeating(nameof(SpawnRandomAnimal), startDelay, Random.Range(3, 5));
    }

    private void SpawnRandomAnimal() {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Transform animal = Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation).GetComponent<Transform>();
        HealthBar hb = Instantiate(healthBar, canvas);
        hb.target = animal;
        Camera cam = Camera.main;
        transform.position = cam.ViewportToScreenPoint(cam.WorldToViewportPoint(animal.position + hb.yOffset * Vector3.up));
        animal.GetComponent<MoveForward>().hb = hb;
    }
}
