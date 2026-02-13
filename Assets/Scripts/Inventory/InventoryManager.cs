using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<ItemSO> inventory = new List<ItemSO>();
    [SerializeField] private int maxInventoryCapacity = 5;
    [SerializeField] private Transform activeItemPos;

    [SerializeField] private InventoryUI inventoryUI;

    private int activeItemIndex = -1;
    private GameObject activeItem;

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
        if (activeItemIndex == -1) return false;
        if (inventoryUI) inventoryUI.RemoveItemUI(activeItemIndex);
        inventory.RemoveAt(activeItemIndex);
        DestroyActiveItem();

        return true;
    }

    public void SetItemActive() 
    { 
        if(activeItemIndex != -1) //remove item if active
        {
            DestroyActiveItem();
        }
        else // first set item to active  
        {
            if (inventory.Count == 0) return; //inventory is empty

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


            if (setPrevious) activeItemIndex = activeItemIndex - 1 < 0 ? inventory.Count - 1 : activeItemIndex - 1;//Previous item
            else activeItemIndex = activeItemIndex + 1 >= inventory.Count ? 0 : activeItemIndex + 1;  //NextItem
            Destroy(activeItem);
            InstantiateActiveItem();        
                
        }

    }

    private void InstantiateActiveItem() 
    {
        ItemSO newItemSO = inventory[activeItemIndex];
        activeItem = Instantiate(newItemSO.ItemPrefab, activeItemPos.position, activeItemPos.rotation);
        activeItem.transform.SetParent(activeItemPos);
    }
    private void DestroyActiveItem() 
    {
        Destroy(activeItem);
        activeItem = null;
        activeItemIndex = -1;
    }

    public ItemSO GetActiveItem() 
    {
        if (activeItemIndex == -1) return null;
        return inventory[activeItemIndex];
    }


    //OLD CODE 
    /*public bool RemoveItem(ItemSO itemSO)
    {
        if (!inventory.Contains(itemSO)) return false; //if existed in the inventory

        if (inventoryUI) inventoryUI.RemoveItemUI(inventory.IndexOf(itemSO));
        inventory.Remove(itemSO);
        return true;
    }
    public bool CheckForItem(ItemSO itemSO)
    {
        if (inventory.Contains(itemSO)) return true;
        return false;
    }*/

}
