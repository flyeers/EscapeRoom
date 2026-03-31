using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private List<ItemSO> inventory = new List<ItemSO>();
    [SerializeField] private int maxInventoryCapacity = 5;
    [SerializeField] private Transform activeItemPos;

    [SerializeField] private InventoryUI inventoryUI;

    [Header("Phone")]
    [SerializeField] private ItemSO phoneItem;
    [SerializeField] private string phoneInitialMessage;

    [Header("Sound")]
    [SerializeField] private AudioClip NotifyClip;
    //[SerializeField] private AudioClip PickUpClip;
    [SerializeField] private AudioClip ItemClip;

    private int activeItemIndex = -1; // -2 == phone , -1 == empty 
    private GameObject activeItem;
    private AudioComponent audioComponent;

    private void Awake()
    {
        audioComponent = gameObject.GetComponent<AudioComponent>();
        if (phoneItem) phoneItem.message = phoneInitialMessage;

        if (inventory.Count == 0 || !inventoryUI) return;
        foreach(var item in inventory) 
        {
            inventoryUI.AddItemUI(item.ItemSprite);
        }
    }

    public bool AddItem(ItemSO itemSO)
    {
        if(inventory.Count == maxInventoryCapacity) return false; //inventory is full
        
        inventory.Add(itemSO);
        if (inventoryUI) inventoryUI.AddItemUI(itemSO.ItemSprite);

        //set as active item 
        if (activeItemIndex != -1) 
        {
            DestroyActiveItem();
        }
        activeItemIndex = inventory.Count - 1;
        InstantiateActiveItem();

        return true;
    }
    
    //Remove active item
    public bool RemoveItem() 
    {
        if (activeItemIndex <= -1) return false;
        if (inventoryUI) inventoryUI.RemoveItemUI(activeItemIndex);
        inventory.RemoveAt(activeItemIndex);
        DestroyActiveItem();

        return true;
    }

    public void SetItemActive() 
    { 
        if(activeItemIndex > -1) //remove item if active
        {
            DestroyActiveItem();
            if (inventoryUI) inventoryUI.SetMessage(null, false);
        }
        else // first item set to active  
        {
            if (inventory.Count == 0) return; //inventory is empty

            if (activeItemIndex == -2) DestroyActiveItem(); //remove phone --> TODO - si quiero q al dar tab si no habia item tambien se quite el movil poner antes de la anterior linea

            activeItemIndex = 0;
            InstantiateActiveItem();
        }
    }
    public void ActivateItem(bool setPrevious) 
    {
        if (inventory.Count == 0) return;
        if(activeItemIndex == -1) //no item active - set first
        {
            SetItemActive();
        }
        else 
        {
            if (inventory.Count == 1) return;

            //deactive UI
            if (inventoryUI) inventoryUI.SetBackgroudSelected(activeItemIndex, false);

            //set new index - destroy old instantiate new item
            if (setPrevious) activeItemIndex = activeItemIndex - 1 < 0 ? inventory.Count - 1 : activeItemIndex - 1;//Previous item
            else activeItemIndex = activeItemIndex + 1 >= inventory.Count ? 0 : activeItemIndex + 1;  //NextItem
            Destroy(activeItem);
            InstantiateActiveItem();

        }

    }

    ///PHONE
    public void SetPhoneActive() 
    {
        if (activeItemIndex == -2) // was phone allready -> destroy 
        { 
            DestroyActiveItem();
            if (inventoryUI) inventoryUI.SetMessage(null, false);
            return;
        }
        
        if (activeItemIndex != -1) //an objet is active
        {
            DestroyActiveItem();
        }
        activeItem = Instantiate(phoneItem.ItemPrefab, activeItemPos.position, activeItemPos.rotation);
        activeItem.transform.SetParent(activeItemPos);
        activeItemIndex = -2;

        //active UI
        if (inventoryUI) inventoryUI.SetBackgroudPnone(true);
        if (inventoryUI) inventoryUI.SetMessage(phoneItem.message, phoneItem.showMessage);
        if (audioComponent) GetComponent<AudioComponent>().PlaySound(ItemClip);
    }
  

    private void InstantiateActiveItem() 
    {
        ItemSO newItemSO = inventory[activeItemIndex];
        activeItem = Instantiate(newItemSO.ItemPrefab, activeItemPos.position, activeItemPos.rotation);
        activeItem.transform.SetParent(activeItemPos);

        //active UI
        if (inventoryUI) inventoryUI.SetBackgroudSelected(activeItemIndex, true);
        if (inventoryUI) inventoryUI.SetMessage(newItemSO.message, newItemSO.showMessage);
        if (audioComponent) GetComponent<AudioComponent>().PlaySound(ItemClip);
    }
    private void DestroyActiveItem() 
    {
        //deactive UI
        if (inventoryUI) inventoryUI.SetBackgroudSelected(activeItemIndex, false);

        Destroy(activeItem);
        activeItem = null;
        activeItemIndex = -1;
    }

    public ItemSO GetActiveItem() 
    {
        if (activeItemIndex == -1) return null;
        return inventory[activeItemIndex];
    }

    public bool CheckForItem(ItemSO item)
    {
        return inventory.Contains(item) || item == phoneItem;
    }

    public void RefreshMessageUI(ItemSO itemSO) 
    {
        if ((inventory.Contains(itemSO) && inventory.IndexOf(itemSO) != activeItemIndex)
            || (itemSO == phoneItem && activeItemIndex == -2)) 
        { 
            if (inventoryUI) inventoryUI.SetMessage(itemSO.message, itemSO.showMessage);
        }
    }
    public void NotifyMessageUI(ItemSO itemSO) 
    {
        if (inventory.Contains(itemSO)) 
        { 
            if (inventoryUI) inventoryUI.SetBackgroundNotify(inventory.IndexOf(itemSO));
            if (audioComponent) GetComponent<AudioComponent>().PlaySound(NotifyClip);
        }
        else if (itemSO == phoneItem) 
        {
            if (inventoryUI) inventoryUI.SetBackgroundNotifyPhone();
            if (audioComponent) GetComponent<AudioComponent>().PlaySound(NotifyClip);
        }

    }

}
