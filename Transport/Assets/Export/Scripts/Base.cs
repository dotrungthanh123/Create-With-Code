using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SimpleJSON;
using UnityEngine;

public struct BaseInfo {
    public int capacity;
}

/// <summary>
/// A special building that hold a static reference so it can be found by other script easily (e.g. for Unit to go back
/// to it)
/// </summary>
public class Base : Building
{ 
    public static Base Instance { get; private set; }
    public Pack pack;
    public Transform packPos;
    public TextAsset baseInfo;

    private BaseInfo info;

    private void Awake()
    {
        Instance = this;
    }

    private void Start() {
        info = JsonUtility.FromJson<BaseInfo>(baseInfo.text);
        InventorySpace = info.capacity;
    }

    public int AddItem(string resourceId, int amount, Color color) {
        if (m_CurrentAmount == InventorySpace) return -1;

        int oldAmount = m_CurrentAmount;
        int leftOverAmount = base.AddItem(resourceId, amount); // add functionality to the base function and keep the return value
        int addedAmount = m_CurrentAmount - oldAmount;

        for (int i = 0; i < addedAmount; i++) {
            int temp = (i + oldAmount) % 16;
            Pack p = Instantiate(pack, packPos);
            p.transform.rotation = Quaternion.Euler(Vector3.zero);
            p.transform.position += Vector3.right * (temp % 4) * 0.5f;
            p.transform.position -= Vector3.forward * Mathf.FloorToInt(temp / 4.0f) * 0.5f;
            p.transform.position += Vector3.up * Mathf.FloorToInt((i + oldAmount) / 16.0f) * 0.5f;
            p.SetColor(color);
        }

        return leftOverAmount;
    }
}
