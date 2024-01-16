using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private UIInventoryDescription itemDescription;

    [SerializeField]
    private MouseFollower mouseFollower;
    List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

    private int _currentlyDraggedItemIndex = -1;

    public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;

    public event Action<int, int> OnSwapItems;
    void Awake(){
        HideInventory();
        itemDescription.ResetDescription();
        mouseFollower.Toggle(false);
    }
    public void InitializeInvetoryUI(int inventorySize){
        for(int i = 0; i < inventorySize; i++){
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel, false);
            listOfUIItems.Add(uiItem);

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
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if(index == -1) return;
        OnDescriptionRequested?.Invoke(index);

    }

    public void ResetSelection(){
        itemDescription.ResetDescription();
        DeselectAllItems();
    }

    private void DeselectAllItems(){
        foreach(UIInventoryItem item in listOfUIItems){
            item.Deselect();
        }
    }

    private void HandleBeginDrag(UIInventoryItem inventoryItemUI){
        //Debug.Log("Item begin drag");
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if(index == -1) return;
        _currentlyDraggedItemIndex = index;
        HandleItemSelection(inventoryItemUI);
       OnStartDragging?.Invoke(index);
    }

    private void HandleSwap(UIInventoryItem inventoryItemUI){
        //Debug.Log("Item dropped on");
        int index = listOfUIItems.IndexOf(inventoryItemUI);
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
        mouseFollower.Toggle(false);
        _currentlyDraggedItemIndex = -1;
    }

    public void CreateDraggedItem(Sprite sprite, int quantity){
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, quantity);
    }

    public void DestroyDraggedItem(){
        mouseFollower.Toggle(false);
    }
    public void UpdateData(int itemIndex, Sprite sprite, int itemQuantity){
        if(listOfUIItems.Count > itemIndex) listOfUIItems[itemIndex].SetData(sprite, itemQuantity);
    }
    private void HandleRShowItemActions(UIInventoryItem inventoryItemUI){
        //Debug.Log("Item right click");
    }

    internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
    {
        itemDescription.SetDescription(itemImage, name, description);
        DeselectAllItems();
        listOfUIItems[itemIndex].Select();
    }

    public void ResetAllItems(){
        foreach(UIInventoryItem item in listOfUIItems){
            item.ResetData();
            item.Deselect();
        }
    }
}
