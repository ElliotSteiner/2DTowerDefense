using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string content;
    public string header;

    

    public void Awake()
    {

      
        
    }

  

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(content);
        TooltipSystem.Show(content, header);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }

    
   
}
