using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem _itemPrefab;

    [SerializeField]
    private RectTransform _contentPanel;

    [SerializeField]
    private UIInventoryDescription _itemDescription;

    [SerializeField]
    private MouseFollower _mouseFollower;
    List<UIInventoryItem> _listOfUIItems = new List<UIInventoryItem>();

    private int _currentlyDraggedItemIndex = -1;

    public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;

    public event Action<int, int> OnSwapItems;
    void Awake(){
        HideInventory();
        _itemDescription.ResetDescription();
        _mouseFollower.Toggle(false);
    }
    public void InitializeInvetoryUI(int inventorySize){
        for(int i = 0; i < inventorySize; i++){
            UIInventoryItem uiItem = Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(_contentPanel, false);
            _listOfUIItems.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMousBtnClick += HandleRShowItemActions;
        }
    }

    public void ShowInventory(){
        gameObject.SetActive(true);
        
        ResetSelection();
    }

    public void HideInventory(){
        gameObject.SetActive(false);
        ResetDraggedItem();
    }

    private void HandleItemSelection(UIInventoryItem inventoryItemUI){
        int index = _listOfUIItems.IndexOf(inventoryItemUI);
        if(index == -1) return;
        OnDescriptionRequested?.Invoke(index);

    }

    public void ResetSelection(){
        _itemDescription.ResetDescription();
        DeselectAllItems();
    }

    private void DeselectAllItems(){
        foreach(UIInventoryItem item in _listOfUIItems){
            item.Deselect();
        }
    }

    private void HandleBeginDrag(UIInventoryItem inventoryItemUI){
        //Debug.Log("Item begin drag");
        int index = _listOfUIItems.IndexOf(inventoryItemUI);
        if(index == -1) return;
        _currentlyDraggedItemIndex = index;
        HandleItemSelection(inventoryItemUI);
       OnStartDragging?.Invoke(index);
    }

    private void HandleSwap(UIInventoryItem inventoryItemUI){
        //Debug.Log("Item dropped on");
        int index = _listOfUIItems.IndexOf(inventoryItemUI);
        if(index == -1) {
            return;
        }
        OnSwapItems?.Invoke(_currentlyDraggedItemIndex, index);
        HandleItemSelection(inventoryItemUI);
    }

    private void HandleEndDrag(UIInventoryItem inventoryItemUI){
        //Debug.Log("Item end drag");
        ResetDraggedItem();
    }

    private void ResetDraggedItem(){
        _mouseFollower.Toggle(false);
        _currentlyDraggedItemIndex = -1;
    }

    public void CreateDraggedItem(Sprite sprite, int quantity){
        _mouseFollower.Toggle(true);
        _mouseFollower.SetData(sprite, quantity);
    }

    public void DestroyDraggedItem(){
        _mouseFollower.Toggle(false);
    }
    public void UpdateData(int itemIndex, Sprite sprite, int itemQuantity){
        if(_listOfUIItems.Count > itemIndex) _listOfUIItems[itemIndex].SetData(sprite, itemQuantity);
    }
    private void HandleRShowItemActions(UIInventoryItem inventoryItemUI){
        //Debug.Log("Item right click");
    }

    internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
    {
        _itemDescription.SetDescription(itemImage, name, description);
        DeselectAllItems();
        _listOfUIItems[itemIndex].Select();
    }

    public void ResetAllItems(){
        foreach(UIInventoryItem item in _listOfUIItems){
            item.ResetData();
            item.Deselect();
        }
    }
}
