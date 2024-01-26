using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
   [SerializeField]
   private Canvas _canvas;
   [SerializeField]
   private Camera _mainCam;

   [SerializeField]
   private UIInventoryItem _item;

   void Awake(){
        _canvas = transform.root.GetComponent<Canvas>();
        _mainCam = Camera.main;
        _item = GetComponentInChildren<UIInventoryItem>();
   }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, Input.mousePosition, _canvas.worldCamera, out position);
        transform.position = _canvas.transform.TransformPoint(position);
    }

    public void SetData(Sprite sprite, int quantity){
        _item.SetData(sprite, quantity);
    }

    public void Toggle(bool val){
        Debug.Log($"Item toggled {val}");
        gameObject.SetActive(val);
    }
}
