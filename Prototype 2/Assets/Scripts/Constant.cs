using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Constant
{
    public static string maxHp = "maxHp";
    public static string score = "score";
    public static string speed = "speed";
    public static string highScore = "highScore";
    public static string animalPath = Path.Combine(Application.dataPath, "Resources/animal_data.json");
    public static string achievementPath = Path.Combine(Application.dataPath, "Resources/achievement.json");

    public static string GetString(this SimpleJSON.JSONNode s, params string[] keys) {
        foreach (string key in keys) {
            s = s[key];
        }
        return s.Value;
    }

    public static int GetInt(this SimpleJSON.JSONNode s, params string[] keys) {
        return int.Parse(s.GetString(keys));
    }

}
