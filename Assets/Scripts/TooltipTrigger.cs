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
       
        if (content.Contains("Upgrades"))
        {

            content = "Upgrades Tower Damage and Range <br> 300 Gold";

        }
    }
    public void Update()
    {
        
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        TooltipSystem.Show(content, header);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
    public void ChangeText()
    {
        
        
        
        if (content.Contains("Upgrades"))
        {

            content = "Upgrades Tower Damage and Range <br> 1000 Gold";

        }
    }
}
