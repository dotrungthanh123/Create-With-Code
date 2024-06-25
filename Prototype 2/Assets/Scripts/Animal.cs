using UnityEngine;

public class Animal : MonoBehaviour {
    public string name;
}

[System.Serializable]
public class AnimalInfo
{
    public int maxHp;
    public int speed;
    public int score;
}