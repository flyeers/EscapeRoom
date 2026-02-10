using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<ItemSO> inventory = new List<ItemSO>();
    [SerializeField] private int maxInventoryCapacity = 5;

    [SerializeField] private InventoryUI inventoryUI;


    public bool AddItem(ItemSO itemSO) 
    {
        if(inventory.Count == maxInventoryCapacity) return false; //inventory is full
        
        inventory.Add(itemSO);
        if (inventoryUI) inventoryUI.AddItemUI(itemSO.ItemSprite);

        return true;
    }

    public bool RemoveItem(ItemSO itemSO) 
    {
        if (!inventory.Contains(itemSO)) return false; //it existed in the inventory
                  
        if (inventoryUI) inventoryUI.RemoveItemUI(inventory.IndexOf(itemSO));
        inventory.Remove(itemSO);
        return true;
    }

    public bool CheckForItem(ItemSO itemSO)
    {
        if (inventory.Contains(itemSO)) return true;
        return false;
    }

}
