using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TeleporterData", menuName = "Teleporter Data", order = 0)]
public class TeleporterData : ScriptableObject
{
    public AudioClip activationClip;

    public string name;
    public string description;
}
