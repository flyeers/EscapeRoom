using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemSO, InventoryItem> itemInstances = new Dictionary<ItemSO, InventoryItem>();


    public void AddItem(ItemSO itemSO) 
    {

        if(itemInstances.TryGetValue(itemSO, out InventoryItem item)) //it existed in the inventory
        {
            item.AddToStack();
        }
        else 
        {
            InventoryItem newItem = new InventoryItem(itemSO);
            inventory.Add(newItem);
            itemInstances.Add(itemSO, newItem);
        }
    }

    public void RemoveItem(ItemSO itemSO) 
    {
        if (itemInstances.TryGetValue(itemSO, out InventoryItem item)) //it existed in the inventory
        {
            item.RemoveFromStack();
            if(item.stackSize == 0)
            { 
                inventory.Remove(item);
                itemInstances.Remove(itemSO);
            }
        }
    }

    public bool CheckForItem(ItemSO itemSO)
    {
        if (itemInstances.TryGetValue(itemSO, out InventoryItem item)) return true;
        return false;
    }

}
