using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Item SO", order = 0)]
public class Item : ScriptableObject
{
    [field : SerializeField]
    public bool isStackable { get; set; }

    public int ID => GetInstanceID();

    [field : SerializeField]
    public int MaxStackSize { get; set; } = 1;

    [field : SerializeField]
    public string Name { get; set; }

    [field : SerializeField]
    [field : TextArea]
    public string Description { get; set; }

    [field : SerializeField]
    public Sprite ItemImage { get; set; }
}
