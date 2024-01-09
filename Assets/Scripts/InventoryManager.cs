using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage inventoryUI;

    public int inventorySize = 10;

    void Start()
    {
        Debug.Log("InventoryManager Start");
        inventoryUI.InitializeInvetoryUI(inventorySize);
    }

    void Update(){
        Debug.Log("InventoryManager update");
        if(Input.GetKeyDown(KeyCode.I)){
            Debug.Log("I key pressed");
            if(inventoryUI.gameObject.activeSelf){
                inventoryUI.HideInventory();
            }else{
                inventoryUI.ShowInventory();
            }
        }
    }
}
