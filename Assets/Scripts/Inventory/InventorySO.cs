using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


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

    public int AddItem(ItemSO item, int quantity){
        if(item.IsStackable==false){ 
            for(int i = 0; i < _inventoryItems.Count; i++){
                while(quantity > 0 && IsInventoryFull() == false){
                    quantity-=AddItemToFirstFreeSlot(item, 1);
                }
            InformAboutChange();
            return quantity;
            }
        }
        quantity = AddStackableItem(item, quantity);
        InformAboutChange();
        return quantity;
    }

    public void AddItem(InventoryItem item){
        AddItem(item.item, item.quantity);
    }

    public int AddItemToFirstFreeSlot(ItemSO item, int quantity){
        InventoryItem newItem = new InventoryItem{
            item = item,
            quantity = quantity
        };

        for(int i = 0; i < _inventoryItems.Count; i++){
            if(_inventoryItems[i].IsEmpty){
                _inventoryItems[i] = newItem;
                return quantity;
            }
        }
        return 0;
    }

    private bool IsInventoryFull()
    => _inventoryItems.Where(item => item.IsEmpty).Any() == false;
    
    private int AddStackableItem(ItemSO item, int quantity){
        for(int i = 0; i < _inventoryItems.Count; i++){
            if(_inventoryItems[i].IsEmpty) continue;
            if(_inventoryItems[i].item.ID == item.ID){
                int amountPossibleToTake = _inventoryItems[i].item.MaxStackSize - _inventoryItems[i].quantity;

                if(quantity > amountPossibleToTake){
                    _inventoryItems[i] = _inventoryItems[i].ChangeQuantity(_inventoryItems[i].item.MaxStackSize);
                    quantity -= amountPossibleToTake;
                }
                else{
                    _inventoryItems[i] = _inventoryItems[i].ChangeQuantity(_inventoryItems[i].quantity + quantity);
                    InformAboutChange();
                    return 0;
                }
            }
        }
        while(quantity > 0 && IsInventoryFull() == false){
           int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
            quantity -= newQuantity;
            AddItemToFirstFreeSlot(item, newQuantity);
        }
        return quantity;
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
