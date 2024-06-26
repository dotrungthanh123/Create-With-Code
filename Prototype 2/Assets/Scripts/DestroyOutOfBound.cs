using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOutOfBound : MonoBehaviour
{
    private float topBound = 30;
    private float lowerBound = -10;
    private Score scoreObject;

    private void Start() {
        scoreObject = FindObjectOfType<Score>();
    }

    void Update() {
        if (transform.position.z > topBound) {
            Destroy(gameObject);
        } else if (transform.position.z < lowerBound) {
            GameOver();
            Destroy(gameObject);
            Destroy(gameObject.GetComponent<MoveForward>().hb.gameObject);
        }
    }

    private void GameOver() {
        string achievement = File.ReadAllText(Constant.achievementPath);
        dynamic jsonObj = JsonConvert.DeserializeObject(achievement);
        if (scoreObject.playerScore > SpawnManager.achievementData.GetInt("highScore")) {
            jsonObj["highScore"] = scoreObject.playerScore;
        }

        jsonObj["time"] += SpawnManager.elapsedTime;
        jsonObj["time"] = (int) jsonObj["time"];

        jsonObj["amount"]["dog"] += MoveForward.animalCount["dog"];
        jsonObj["amount"]["fox"] += MoveForward.animalCount["fox"];
        jsonObj["amount"]["moose"] += MoveForward.animalCount["moose"];

        string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
        File.WriteAllText(Constant.achievementPath, output);

        SceneManager.LoadScene(0);

    }
}
