using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        string contents = "{ '541065216': { 'speed': 5, 'amount': 1 }, '-392943488': { 'speed': 4, 'amount': 2 }, '-548816128': { 'speed': 3, 'amount': 3 }, '-572159260': { 'speed': 2, 'amount': 4 }, 'capacity': 10 }".Replace("'", "\"");
        Directory.CreateDirectory(Path.GetDirectoryName(Constant.infoPath));
        File.WriteAllText(Constant.infoPath, contents);
    }

    private void Start() {
        string info = File.ReadAllText(Constant.infoPath);
        InventorySpace = JSON.Parse(info).GetInt("capacity");
    }

    public override int AddItem(string resourceId, int amount) {
        if (m_CurrentAmount == InventorySpace) return -1;

        int oldAmount = m_CurrentAmount;
        int leftOverAmount = base.AddItem(resourceId, amount); // add functionality to the base function and keep the return value
        int addedAmount = m_CurrentAmount - oldAmount;

        for (int i = 0; i < addedAmount; i++) {
            Pack p = Instantiate(pack, packPos);
            p.transform.rotation = Quaternion.Euler(Vector3.zero);
            p.transform.position += Vector3.right * ((oldAmount + i) % 4) * 0.5f;
            p.transform.position -= Vector3.forward * Mathf.FloorToInt((oldAmount + i) / (float) 4) * 0.5f;
        }

        return leftOverAmount;
    }
}
