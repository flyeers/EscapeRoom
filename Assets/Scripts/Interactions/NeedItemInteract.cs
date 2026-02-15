using Assets.Scripts.Interactions;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class NeedItemInteract: MonoBehaviour, IInteractable
{
    [SerializeField] protected List<ItemSO> itemsSO = new List<ItemSO>(1);
    [SerializeField] protected Action action;
    [SerializeField] protected bool deactivateAfterAction = false;

    [Header("Alternative message")]
    [SerializeField] private bool showAlternativeMessage = true;
    [SerializeField] private ShowMessageSO showMessageSO;
    public string messageText = "";

    protected ItemSO itemSO;
    protected InventoryManager inventory;

    public void Interact(GameObject interactor)
    {
        inventory = interactor.gameObject.GetComponentInChildren<InventoryManager>();
        if (inventory)
        {
            itemSO = inventory.GetActiveItem();
            if (itemSO && itemsSO.Count != 0 && itemsSO.Contains(itemSO)) 
            {
                UseItem(interactor);
            }
            else 
            {
                Debug.Log("NO ITEM");
                if(showMessageSO) showMessageSO.ShowMessage(messageText);
            }
        }
    }

    protected virtual void UseItem(GameObject interactor) 
    {
        if (itemSO.consumable) inventory.RemoveItem(); //if consumable remove from inventory

        if (action) action.ExecuteAction(null);
        Debug.Log("OPEEEN");

        if (deactivateAfterAction)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
