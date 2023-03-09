using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOverlay : MonoBehaviour
{
    private static UpgradeOverlay Instance { get; set; }

    private Tower tower;
    GameObject Overlay;
    static GameObject Button;

    private void Awake()
    {
        Overlay = GameObject.Find("UpgradeOverlay");
        //Debug.Log(Overlay);
        Button = Overlay.transform.GetChild(0).GetChild(0).gameObject;
        //Debug.Log(Button);
        Instance = this;

        Hide();
    }

    public static void Show_Static(Tower tower)
    {
        
        Instance.Show(tower);
    }
    public static void Hide_Static()
    {
        Instance.Hide();
    }

    private void Show(Tower tower)
    { 
        

        this.tower = tower;
        gameObject.SetActive(true);
        transform.position = tower.transform.position;
        RefreshRangeVisuals();
    }

    private void Hide()

    {
        
        
        gameObject.SetActive(false);
    }

    private void RefreshRangeVisuals()
    {
        transform.Find("Range").localScale = Vector3.one * tower.GetRange() * .9f;
    }

    public void UpgradeTower()
    {
        Debug.Log("Clicked!");
        tower.upgradeTower();
        RefreshRangeVisuals();
    }

    public static void HideButton()
    {
        Button.SetActive(false);
    }
    public static void ShowButton()
    {
        Button.SetActive(true);
    }
}
