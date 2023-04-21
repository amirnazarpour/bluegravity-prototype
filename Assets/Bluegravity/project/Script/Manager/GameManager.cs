using System;
using Bluegravity.Character;
using Bluegravity.Shop;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Controller controller;
    public Character character;
    public ShopManager shopManager;
    
    private void Start()
    {
        controller.OnShop += OpenShop;
        shopManager.OnClose += CloseShop;
    }

    public void CloseShop()
    {
        character.SetPart();
        controller.gameObject.SetActive(true);
    }

    public void OpenShop()
    {
        controller.gameObject.SetActive(false);
        shopManager.gameObject.SetActive(true);
    }
}