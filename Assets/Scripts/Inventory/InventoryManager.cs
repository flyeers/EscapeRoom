using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<InventoryItem> inventory = new List<InventoryItem>();
    [SerializeField] private int maxInventoryCapacity = 5;
    private Dictionary<ItemSO, InventoryItem> itemInstances = new Dictionary<ItemSO, InventoryItem>();

    [SerializeField] private InventoryUI inventoryUI;


    public bool AddItem(ItemSO itemSO) 
    {
        if(inventory.Count == maxInventoryCapacity) return false; //inventory is full

        if(itemInstances.TryGetValue(itemSO, out InventoryItem item)) //it existed in the inventory
        {
            item.AddToStack();

            if(inventoryUI) inventoryUI.AddStackUI(inventory.IndexOf(item));
        }
        else 
        {
            InventoryItem newItem = new InventoryItem(itemSO);
            inventory.Add(newItem);
            itemInstances.Add(itemSO, newItem);

            if (inventoryUI) inventoryUI.AddItemUI(itemSO.ItemSprite);
        }

        return true;
    }

    public void RemoveItem(ItemSO itemSO) 
    {
        if (itemInstances.TryGetValue(itemSO, out InventoryItem item)) //it existed in the inventory
        {
            item.RemoveFromStack();
            if (item.stackSize == 0)
            {
                if (inventoryUI) inventoryUI.RemoveItemUI(inventory.IndexOf(item));

                inventory.Remove(item);
                itemInstances.Remove(itemSO);
            }
            else 
            {
                if (inventoryUI) inventoryUI.RemoveStackUI(inventory.IndexOf(item));
            }
        }
    }

    public bool CheckForItem(ItemSO itemSO)
    {
        if (itemInstances.TryGetValue(itemSO, out InventoryItem item)) return true;
        return false;
    }

}
