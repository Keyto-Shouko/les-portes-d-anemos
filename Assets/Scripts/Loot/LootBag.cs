using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject lootItemPrefab;
    public List<ItemSO> lootTable = new List<ItemSO>();

    // Start is called before the first frame update
    ItemSO GetDroppedItem()
    {
        int randomNumber = Random.Range(0, 101);
        Debug.Log("Random number: " + randomNumber);
        List<ItemSO> possibleItems = new List<ItemSO>();
        foreach (ItemSO loot in lootTable)
        {
            // Here is the logic, if the random number is less than the drop chance, add the item to the possible items list.
            // Consider the drop chance as a percentage and the random number as a threshold that must be below it.
            if (randomNumber <= loot.DropChance)
            {
                possibleItems.Add(loot);
            }
        }
        if (possibleItems.Count > 0)
        {
            ItemSO droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        Debug.Log("No item dropped");
        return null;
    }

    // Call this method to instantiate loot with a delay
    public void InstantiateLoot(Vector3 spawnPosition, float delayBeforeDrop = 0.0f)
    {
        StartCoroutine(DelayedInstantiateLoot(spawnPosition, delayBeforeDrop));
    }

    // Coroutine to handle delay and instantiation
    private IEnumerator DelayedInstantiateLoot(Vector3 spawnPosition, float delayBeforeDrop)
    {
        Debug.Log("Delaying loot drop by " + delayBeforeDrop + " seconds");
        yield return new WaitForSeconds(delayBeforeDrop);

        ItemSO droppedItem = GetDroppedItem();
        if (droppedItem != null)
        {
            // Instantiate the loot item after the delay
            GameObject lootGameObject = Instantiate(lootItemPrefab, spawnPosition, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.ItemImage;
            lootGameObject.GetComponent<Item>().InventoryItem = droppedItem;
            Debug.Log("Dropped item: " + droppedItem.name);
        }
    }

    // Function to destroy the object if needed
    public void DestroyEntity()
    {
        Destroy(gameObject);
    }
}
