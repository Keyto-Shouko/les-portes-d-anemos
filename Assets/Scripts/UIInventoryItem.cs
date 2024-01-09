using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIInventoryItem : MonoBehaviour
{

    [SerializeField]
    private Image _itemImage;

    [SerializeField]
    private TMP_Text _quantityText;

    [SerializeField]
    private Image _borderImage;

    public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeingDrag, OnItemEndDrag;

    private bool empty = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
