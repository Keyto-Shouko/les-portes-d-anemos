using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();


    public void InitializeInvetoryUI(int inventorySize){
        for(int i = 0; i < inventorySize; i++){
            UIInventoryItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(contentPanel, false);
            listOfUIItems.Add(item);
        }
    }

    public void ShowInventory(){
        gameObject.SetActive(true);
    }

    public void HideInventory(){
        gameObject.SetActive(false);
    }
}
