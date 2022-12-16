using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOverlay : MonoBehaviour
{
    private static UpgradeOverlay Instance { get; set; }

    private Tower tower;

    private void Awake()
    {
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
        transform.Find("Range").localScale = Vector3.one * tower.GetRange() * 1f;
    }
}
