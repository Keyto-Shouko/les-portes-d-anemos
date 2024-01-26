using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage _inventoryUI;

    [SerializeField]
    private InventorySO _inventoryData;

    public List<InventoryItem> initialItems = new List<InventoryItem>();

    void Start()
    {
        PrepareUI();
        PrepareInventoryData();
    }

    private void PrepareUI(){
        _inventoryUI.InitializeInvetoryUI(_inventoryData.Size);
        this._inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
        this._inventoryUI.OnSwapItems += HandleSwapItems;
        this._inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        this._inventoryUI.OnStartDragging += HandleDragging;

    }

    private void PrepareInventoryData(){
        _inventoryData.Initialize();
        _inventoryData.OnInventoryUpdated += UpdatedInventoryUI;
        foreach(InventoryItem item in initialItems){
            if(item.IsEmpty) continue;
            _inventoryData.AddItem(item);
        }
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.I)){
            if(_inventoryUI.isActiveAndEnabled == false){
                _inventoryUI.ShowInventory();
                foreach(var item in _inventoryData.GetInventoryState()){
                    _inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                }
            }else{
                _inventoryUI.HideInventory();
            }
        }
    }

    private void HandleDescriptionRequest(int itemIndex){
        InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
        if(inventoryItem.IsEmpty) {
            _inventoryUI.ResetSelection();
            return;   
        }
        ItemSO item = inventoryItem.item;
        _inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description);
    }

    private void HandleSwapItems(int itemIndex1, int itemIndex2){
        _inventoryData.SwapItems(itemIndex1, itemIndex2);
        _inventoryUI.DestroyDraggedItem();
    }

    private void HandleItemActionRequest(int index){
       
    }

    private void HandleDragging(int index){
       InventoryItem inventoryItem = _inventoryData.GetItemAt(index);
       if(inventoryItem.IsEmpty) return;

       _inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
    }

    private void UpdatedInventoryUI(Dictionary<int, InventoryItem> inventoryState){
        _inventoryUI.ResetAllItems();
        foreach(var item in inventoryState){
            _inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
        }
    }
}
