using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RupeeData", menuName = "Rupee Data", order = 0)]
public class RupeeData : ScriptableObject
{
    public Color color;
    public int score;
    public float speed;
    public AudioClip collectClip;
}
