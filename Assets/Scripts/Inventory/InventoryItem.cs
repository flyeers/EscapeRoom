using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemSO itemSo;
    public int stackSize;

    public InventoryItem(ItemSO itemSo)
    {
        this.itemSo = itemSo;
        AddToStack();
    }   

    public void AddToStack() 
    {
        stackSize++;
    }

    public void RemoveFromStack() 
    {
        stackSize--;
    }
}
