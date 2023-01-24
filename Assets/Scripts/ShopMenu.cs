using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{

    [SerializeField] GameObject shopMenu;
    [SerializeField] GameObject backButton;

    public void Shop() {
        shopMenu.SetActive(true);
    }
    public void Back() {
        shopMenu.SetActive(false);
    }
}
