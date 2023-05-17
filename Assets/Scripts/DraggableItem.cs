using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform parentAfterDrag;
    private Vector3 startPoint;
    public int itemID;


    public void OnBeginDrag(PointerEventData eventData)
    {
        startPoint = transform.position;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, Camera.main.ScreenToWorldPoint(Input.mousePosition).z + 10);
        if (inventoryManager.shopItems[3, itemID] > 0)
        {
            inventoryManager.ItemEffect(itemID, position);
        }
        position = transform.position;
        transform.SetParent(parentAfterDrag);

        //   if (MapManager.GetTileType() == false)
        //   {
        //       transform.position = startPoint;

        // |##### Not sure how to call the GetTileType script #####| 

        //   } 
        //   else
        //   {
        gameObject.BroadcastMessage("ItemUse", itemID);
        Destroy(gameObject);
        //   }
    }
}
