using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SimpleJSON;
using UnityEngine;

/// <summary>
/// A special building that hold a static reference so it can be found by other script easily (e.g. for Unit to go back
/// to it)
/// </summary>
public class Base : Building
{ 
    public static Base Instance { get; private set; }
    public Pack pack;
    public Transform packPos;

    private void Awake()
    {
        Instance = this;
        string contents = "{ '541065216': { 'speed': 5, 'amount': 1 }, '-392943488': { 'speed': 4, 'amount': 2 }, '-548816128': { 'speed': 3, 'amount': 3 }, '-572159260': { 'speed': 2, 'amount': 4 }, 'capacity': 100 }".Replace("'", "\"");
        FileStream fileStream = new FileStream(Constant.infoPath, FileMode.Create);
        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(contents);
        }
    }

    private void Start() {
        string info = File.ReadAllText(Constant.infoPath);
        InventorySpace = JSON.Parse(info).GetInt("capacity");
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
