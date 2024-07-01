using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterManager : MonoBehaviour
{   
    public TextAsset[] transporterInfos;

    private TransporterUnit[] transporters;

    private void Start() {
        transporters = FindObjectsOfType<TransporterUnit>();

        TransporterInfo target = JsonUtility.FromJson<TransporterInfo>(transporterInfos[ColorPicker.availableColors.IndexOf(MainManager.Instance.TeamColor)].text);

        foreach (var transporter in transporters) {
            transporter.InitInfo(target);
        }
    }
}
