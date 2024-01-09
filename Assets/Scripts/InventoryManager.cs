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
        inventoryUI.InitializeInvetoryUI(inventorySize);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.I)){
            if(inventoryUI.gameObject.activeSelf){
                inventoryUI.HideInventory();
            }else{
                inventoryUI.ShowInventory();
            }
        }
    }
}
