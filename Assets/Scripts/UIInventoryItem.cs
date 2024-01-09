using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDropHandler, IEndDragHandler, IDragHandler
{

    [SerializeField]
    private Image _itemImage;

    [SerializeField]
    private TMP_Text _quantityText;

    [SerializeField]
    private Image _borderImage;

    public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMousBtnClick;

    private bool empty = true;


    void Awake(){
        ResetData();
        Deselect();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetData(){
        this._itemImage.gameObject.SetActive(false);
        empty = true;
    }

    public void Deselect(){
        _borderImage.enabled = false;
    }

    public void SetData(Sprite sprite, int quantity){
        this._itemImage.gameObject.SetActive(true);
        this._itemImage.sprite = sprite;
        this._quantityText.text = quantity+ "";
        empty = false;
    }

    public void Select(){
        _borderImage.enabled = true;
    }

    public void OnBeginDrag(PointerEventData eventData){
        if(empty) return;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData){
        if(empty) return;
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData){
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnPointerClick(PointerEventData pointerData){
        if(pointerData.button == PointerEventData.InputButton.Right){
            OnRightMousBtnClick?.Invoke(this);
        } else {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnDrag(PointerEventData eventData){
    }
}
