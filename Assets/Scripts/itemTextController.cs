using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemTextController : MonoBehaviour
{
    public Transform popuptext;
    public static string textstatus = "off";

    public void mouseEnter()
    {
        Debug.Log("Collided!");
        if(textstatus == "off")
        {
            if(gameObject.name == "Cannon")
            {
                popuptext.GetComponent<TextMesh>().text = "Shoots Cannonballs\nHigh Damage, Slow Speed\n 100 gold";
            }
            if (gameObject.name == "Wizard")
            {
                popuptext.GetComponent<TextMesh>().text = "Shoots Magic Bolts\nMedium Damage, Medium Speed\n 100 gold";
            }
            if (gameObject.name == "Druid")
            {
                popuptext.GetComponent<TextMesh>().text = "Shoots Vines that slow enemies\nLow Damage, Slow Speed\n 100 gold";
            }
            if (gameObject.name == "Lookout")
            {
                popuptext.GetComponent<TextMesh>().text = "Supports other towers\nIncreased Range and Speed\n 100 gold";
            }
            if (gameObject.name == "Archer")
            {
                popuptext.GetComponent<TextMesh>().text = "Shoots Arrows\nLow Damage, Fast speed\n 100 gold";
            }
            if (gameObject.name == "Boulder")
            {
                popuptext.GetComponent<TextMesh>().text = "Shoots AOE damage boulders\nMedium Damage, Slow speed\n 100 gold";
            }

            textstatus = "on";
            Instantiate(popuptext, new Vector3(transform.position.x, transform.position.y + 2, 0), popuptext.rotation);
        }
    }
    public void mouseExit()
    {
        if(textstatus == "on")
        {
            textstatus = "off";
                
        }
    }
}
