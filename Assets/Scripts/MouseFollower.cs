using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
   [SerializeField]
   private Canvas canvas;
   [SerializeField]
   private Camera mainCam;

   [SerializeField]
   private UIInventoryItem item;

   void Awake(){
        canvas = transform.root.GetComponent<Canvas>();
        mainCam = Camera.main;
        item = GetComponentInChildren<UIInventoryItem>();
   }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out position);
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void SetData(Sprite sprite, int quantity){
        item.SetData(sprite, quantity);
    }

    public void Toggle(bool val){
        Debug.Log($"Item toggled {val}");
        gameObject.SetActive(val);
    }
}
