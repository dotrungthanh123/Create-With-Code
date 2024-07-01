using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Constant {
    public static string GetString(this SimpleJSON.JSONNode s, params string[] keys) {
        foreach (string key in keys) {
            s = s[key];
        }
        return s.Value;
    }

    public static int GetInt(this SimpleJSON.JSONNode s, params string[] keys) {
        return int.Parse(s.GetString(keys));
    }

    public static int IndexOf(this Color[] colors, Color color) {
        for (int i = 0; i < colors.Length; i++) {
            if (color == colors[i]) return i;
        }

        return -1;
    }
}
