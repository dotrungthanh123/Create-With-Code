using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{

    public float speed = 40;
    public int maxHp;
    public int score;
    public HealthBar hb;
    public static Dictionary<string, int> animalCount = new Dictionary<string, int>() {
        {"dog", 0},
        {"fox", 0},
        {"moose", 0},
    };

    private int hp;
    private AnimalInfo info;    
    private Score scoreObject;
    private string animalName;

    private void Start() {
        info = GetComponent<Animal>().info;
        maxHp = info.maxHp;
        score = info.score;
        speed = info.speed;
        animalName = GetComponent<Animal>().animalName;

        hp = maxHp;

        scoreObject = FindObjectOfType<Score>();
    }

    private void Update() {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public void OnHit() {
        hp--;
        hb.UpdateHp((float) hp / maxHp); // bullet does not have hb
        if (hp == 0) {
            scoreObject.IncreaseScore(score);

            animalCount[animalName]++;

            Destroy(gameObject);
            Destroy(hb.gameObject);
        }
    }
}