using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;

public class SpawnManager : MonoBehaviour
{
    public MoveForward[] animalPrefabs;
    public HealthBar healthBar;
    public static float elapsedTime;

    private float spawnRangeX = 10;
    private float spawnPosZ = 20;
    private float startDelay = 2;
    private float spawnInterval = 1.5f;
    private Transform canvas;

    public static JSONNode animalData;
    public static JSONNode achievementData;

    private void Awake() {
        string infos = File.ReadAllText(Constant.animalPath);
        animalData = JSON.Parse(infos);
        infos = File.ReadAllText(Constant.achievementPath);
        achievementData = JSON.Parse(infos);
    }

    private void Start() {
        canvas = GameObject.Find("Canvas").GetComponent<Transform>();
        InvokeRepeating(nameof(SpawnRandomAnimal), startDelay, Random.Range(3, 5));
        elapsedTime = 0;
    }

    private void Update() {
        elapsedTime += Time.deltaTime;
    }

    private void SpawnRandomAnimal() {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Transform animal = Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation).transform;
        HealthBar hb = Instantiate(healthBar, canvas);
        hb.target = animal;
        Camera cam = Camera.main;
        transform.position = cam.ViewportToScreenPoint(cam.WorldToViewportPoint(animal.position + hb.yOffset * Vector3.up));
        animal.GetComponent<MoveForward>().hb = hb;
    }
}
