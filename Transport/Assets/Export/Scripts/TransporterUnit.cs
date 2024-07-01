using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using UnityEngine;

public struct TransporterInfo {

    public TransporterInfo(TransporterInfo info) {
        speed = info.speed;
        capacity = info.capacity;
    }

    public float speed;
    public int capacity;
}

/// <summary>
/// Subclass of Unit that will transport resource from a Resource Pile back to Base.
/// </summary>
public class TransporterUnit : Unit
{
    public int MaxAmountTransported = 1;
    public Transform packTr;   
    public Pack pack;

    private Building m_CurrentTransportTarget;
    private Building.InventoryEntry m_Transporting = new Building.InventoryEntry();

    private Vector3 waitingPosition;
    private Building waitingBuilding;

    private bool isPosWait = false;
    private bool isBuildWait = false;

    private List<Pack> packs = new List<Pack>();
    private bool loading;
    private Color transportingColor;

    private TransporterInfo info;

    protected override void Start()
    {
        base.Start();
        MaxAmountTransported = info.capacity;
        m_Agent.speed = info.speed;
    }

    public void InitInfo(TransporterInfo info) {
        this.info = new TransporterInfo(info);
    }

    // We override the GoTo function to remove the current transport target, as any go to order will cancel the transport
    public override void GoTo(Vector3 position)
    {
        if (m_Transporting.Count > 0) {
            isBuildWait = false;
            isPosWait = true;
            waitingPosition = position;
        } else {
            base.GoTo(position);
            m_CurrentTransportTarget = null;
            isPosWait = false;
        }
    }

    public override void GoTo(Building target)
    {
        if (m_Transporting.Count > 0 && target != Base.Instance) {
            waitingBuilding = target;
            isBuildWait = true;
            isPosWait = false;

            base.GoTo(Base.Instance);
        } else {
            base.GoTo(target);
            isBuildWait = false;
        }
    }

    protected override void Update() {
        if (m_Target != null)
        {
            float distance = Vector3.Distance(m_Target.transform.position, transform.position);

            if (distance < 3.5f)
            {
                m_Agent.isStopped = true;
                if (!loading) {
                    Invoke(nameof(BuildingInRange), 0.25f);
                    loading = true;
                }
            }
        }
    }

    protected override void BuildingInRange()
    {
        if (m_Target == Base.Instance)
        {
            //we arrive at the base, unload!
            if (m_Transporting.Count > 0)
            {
                int leftOverAmount = ((Base) m_Target).AddItem(m_Transporting.ResourceId, m_Transporting.Count, transportingColor);
                int deliveredAmount = m_Transporting.Count;

                if (leftOverAmount == -1) return;

                if (leftOverAmount > 0) {
                    deliveredAmount -= leftOverAmount;
                    m_Transporting.Count = leftOverAmount;
                } else {
                    m_Transporting.ResourceId = "";
                    m_Transporting.Count = 0;

                    if (isPosWait)
                    {
                        GoTo(waitingPosition);
                    }
                    else if (isBuildWait)
                    {
                        GoTo(waitingBuilding);
                    }
                    else
                    {
                        GoTo(m_CurrentTransportTarget);
                    }
                }

                for (int i = packs.Count - 1; i >= packs.Count - deliveredAmount; i--) {
                    Destroy(packs[i].gameObject);
                }

                for (int i = 0; i < deliveredAmount; i++) {
                    packs.RemoveAt(packs.Count - 1);
                }
            }
        }
        else
        {
            if (m_Target != null && m_Target.Inventory.Count > 0 && Vector3.Distance(transform.position, m_Target.transform.position) < 3.5f)
            {
                m_Transporting.ResourceId = m_Target.Inventory[0].ResourceId;
                m_Transporting.Count = m_Target.GetItem(m_Transporting.ResourceId, MaxAmountTransported);
                m_CurrentTransportTarget = m_Target;

                transportingColor = ((ResourcePile) m_Target).color;

                for (int i = 0; i < m_Transporting.Count; i++)
                {
                    packs.Add(Instantiate(pack, packTr));
                    packs[packs.Count - 1].transform.localRotation = Quaternion.Euler(Vector3.zero);
                    packs[packs.Count - 1].transform.position = packTr.position + packTr.right * (i % 3) * 0.25f + packTr.forward * .25f * Mathf.FloorToInt(i / 3);
                    packs[packs.Count - 1].SetColor(transportingColor);
                }

                GoTo(Base.Instance);
            }
        }

        loading = false;
    }
    
    //Override all the UI function to give a new name and display what it is currently transporting
    public override string GetName()
    {
        return "Transporter";
    }

    public override string GetData()
    {
        return $"Can transport up to {MaxAmountTransported}";
    }

    public override void GetContent(ref List<Building.InventoryEntry> content)
    {
        if (m_Transporting.Count > 0)
            content.Add(m_Transporting);
    }
}
