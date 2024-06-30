using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Constant {
    public static string infoPath = Path.Combine(Application.dataPath, "Resources/info.json");

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
