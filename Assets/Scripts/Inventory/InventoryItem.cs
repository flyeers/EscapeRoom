using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemSO itemSo;
    public InventoryItem(ItemSO itemSo)
    {
        this.itemSo = itemSo;
    }   
}
