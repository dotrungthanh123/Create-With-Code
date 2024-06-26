using UnityEngine;

public class Animal : MonoBehaviour {
    public string animalName;
    public AnimalInfo info;

    private void Start() {
        info = new AnimalInfo() {
            maxHp = GetInt(Constant.maxHp),
            speed = GetInt(Constant.speed),
            score = GetInt(Constant.score),
        };
    }

    private int GetInt(string type) {
        return SpawnManager.animalData.GetInt(animalName, type);
    }
}

[System.Serializable]
public struct AnimalInfo
{
    public int maxHp;
    public int speed;
    public int score;
}