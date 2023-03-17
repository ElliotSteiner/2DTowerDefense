using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopTipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject tipWindow;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        tipWindow.SetActive(true);
    }


    public void OnPointerExit(PointerEventData pointerEventData)
    {
        tipWindow.SetActive(false);
    }
}
