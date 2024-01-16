using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage inventoryUI;

    [SerializeField]
    private InventorySO inventoryData;

    public List<InventoryItem> initialItems = new List<InventoryItem>();

    void Start()
    {
        PrepareUI();
        PrepareInventoryData();
    }

    private void PrepareUI(){
        inventoryUI.InitializeInvetoryUI(inventoryData.Size);
        this.inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
        this.inventoryUI.OnSwapItems += HandleSwapItems;
        this.inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        this.inventoryUI.OnStartDragging += HandleDragging;

    }

    private void PrepareInventoryData(){
        inventoryData.Initialize();
        inventoryData.OnInventoryUpdated += UpdatedInventoryUI;
        foreach(InventoryItem item in initialItems){
            if(item.IsEmpty) continue;
            inventoryData.AddItem(item);
        }
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.I)){
            if(inventoryUI.isActiveAndEnabled == false){
                inventoryUI.ShowInventory();
                foreach(var item in inventoryData.GetInventoryState()){
                    inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                }
            }else{
                inventoryUI.HideInventory();
            }
        }
    }

    private void HandleDescriptionRequest(int itemIndex){
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if(inventoryItem.IsEmpty) {
            inventoryUI.ResetSelection();
            return;   
        }
        ItemSO item = inventoryItem.item;
        inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description);
    }

    private void HandleSwapItems(int itemIndex1, int itemIndex2){
        inventoryData.SwapItems(itemIndex1, itemIndex2);
        inventoryUI.DestroyDraggedItem();
    }

    private void HandleItemActionRequest(int index){
       
    }

    private void HandleDragging(int index){
       InventoryItem inventoryItem = inventoryData.GetItemAt(index);
       if(inventoryItem.IsEmpty) return;

       inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
    }

    private void UpdatedInventoryUI(Dictionary<int, InventoryItem> inventoryState){
        inventoryUI.ResetAllItems();
        foreach(var item in inventoryState){
            inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
        }
    }
}
