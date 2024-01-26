using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = " Loot", menuName = "Loot")]
public class Loot : ScriptableObject
{
    //get the item prefab we already made for our inventory system
    public GameObject lootItemPrefab;
    public int dropChance;

    public Loot(GameObject lootItemPrefab, int dropChance)
    {
        this.lootItemPrefab = lootItemPrefab;
        this.dropChance = dropChance;
    }
}
