using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject lootItemPrefab;
    public List<ItemSO> lootTable = new List<ItemSO>();
    // Start is called before the first frame update
    ItemSO GetDroppedItem(){
        int randomNumber = Random.Range(0, 101);
        Debug.Log("Random number: " + randomNumber);
        List<ItemSO> possibleItems = new List<ItemSO>();
        foreach(ItemSO loot in lootTable){
            //here is the logic, if the random number is less than the drop chance, add the item to the possible items list. Consider the drop chance as a percentage and the random number as a threshold that must be below it.
            if(randomNumber <= loot.DropChance){
                possibleItems.Add(loot);
            }
        }
        if(possibleItems.Count > 0){
            ItemSO droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        Debug.Log("No item dropped");
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPosition){
        ItemSO droppedItem = GetDroppedItem();
        if(droppedItem != null){
            GameObject lootGameObject = Instantiate(lootItemPrefab, spawnPosition, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.ItemImage;
            lootGameObject.GetComponent<Item>().InventoryItem = droppedItem;
            Debug.Log("Dropped item: " + droppedItem.name);
        }
    }
}
