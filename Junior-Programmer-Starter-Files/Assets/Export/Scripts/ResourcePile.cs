using UnityEngine;
using SimpleJSON;
using System.IO;

/// <summary>
/// A subclass of Building that produce resource at a constant rate.
/// </summary>
public class ResourcePile : Building
{
    public ResourceItem Item;
    public Color color;

    private float m_ProductionSpeed = 0.5f;
    public float ProductionSpeed 
    {
        set 
        {
            if (value < 0) {
                Debug.LogError("You can't set a negative production speed!");
            } else {
                m_ProductionSpeed = value;
            }
        }
        get => m_ProductionSpeed;
    }

    private float m_CurrentProduction = 0.0f;

    private void Update()
    {
        if (m_CurrentProduction > 1.0f)
        {
            int amountToAdd = Mathf.FloorToInt(m_CurrentProduction);
            int leftOver = AddItem(Item.Id, amountToAdd);

            m_CurrentProduction = m_CurrentProduction - amountToAdd + leftOver;
        }
        
        if (m_CurrentProduction < 1.0f)
        {
            m_CurrentProduction += m_ProductionSpeed * Time.deltaTime;
        }
    }

    public override string GetData()
    {
        return $"Producing at the speed of {m_ProductionSpeed}/s";
    }
    
}