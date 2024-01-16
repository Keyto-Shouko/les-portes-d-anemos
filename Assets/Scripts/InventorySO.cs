using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "InventorySO", menuName = "Inventory SO", order = 0)]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    private List<InventoryItem> _inventoryItems;

    [field : SerializeField]
    public int Size { get; set; } = 10;

    public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

    public void Initialize(){
        _inventoryItems = new List<InventoryItem>();
        for(int i = 0; i < Size; i++){
            _inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
    }

    public void AddItem(ItemSO item, int quantity){
         for(int i = 0; i < _inventoryItems.Count; i++){
            if(_inventoryItems[i].IsEmpty){
                _inventoryItems[i] = new InventoryItem{
                    item = item,
                    quantity = quantity
                };
                return;
            }
        }
    }

    public void AddItem(InventoryItem item){
        AddItem(item.item, item.quantity);
    }

    public Dictionary<int, InventoryItem> GetInventoryState(){
        Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
        for(int i = 0; i < _inventoryItems.Count; i++){
            if(_inventoryItems[i].IsEmpty) continue;
            returnValue[i] = _inventoryItems[i];
        }
        return returnValue;
    }

    public InventoryItem GetItemAt(int itemIndex){
        return _inventoryItems[itemIndex];
    }

    public void SwapItems(int itemIndex1, int itemIndex2){
        InventoryItem temp = _inventoryItems[itemIndex1];
        _inventoryItems[itemIndex1] = _inventoryItems[itemIndex2];
        _inventoryItems[itemIndex2] = temp;
        InformAboutChange();
    }

    private void InformAboutChange(){
        OnInventoryUpdated?.Invoke(GetInventoryState());
    }
}

[Serializable]
public struct InventoryItem
{
    public ItemSO item;
    public int quantity;

    public bool IsEmpty => item == null;

    public InventoryItem ChangeQuantity(int newQuantity){
        return new InventoryItem{
            item = this.item,
            quantity = newQuantity
        };
    }

    public static InventoryItem GetEmptyItem()
        => new InventoryItem{
            item = null,
            quantity = 0
        };

    
}
