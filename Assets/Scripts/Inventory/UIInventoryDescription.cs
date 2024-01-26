using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIInventoryDescription : MonoBehaviour
{
    [SerializeField]
    private Image _itemImage;

    [SerializeField]
    private TMP_Text _title;

    [SerializeField]
    private TMP_Text _description;

    void Awake(){
        ResetDescription();
    }

    void Start()
    {
        
    }

    public void ResetDescription(){
        this._itemImage.gameObject.SetActive(false);
        this._title.text = "";
        this._description.text = "";
    }

    public void SetDescription(Sprite sprite, string itemName, string itemDescription){
        this._itemImage.gameObject.SetActive(true);
        this._itemImage.sprite = sprite;
        this._title.text = itemName;
        this._description.text = itemDescription;
    }
}
