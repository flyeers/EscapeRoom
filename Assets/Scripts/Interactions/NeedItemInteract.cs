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
    [SerializeField] private bool forceAlternativeMessage = false;
    [SerializeField] private ShowMessageSO showMessageSO;
    [TextArea(2, 10)]
    [SerializeField] private string messageText = "";

    protected ItemSO itemSO;
    protected InventoryManager inventory;

    public void Interact(GameObject interactor)
    {
        //if force message - show and exit
        if (forceAlternativeMessage) 
        { 
            if (showMessageSO) showMessageSO.ShowMessage(messageText);
            return;
        }

        inventory = interactor.gameObject.GetComponentInChildren<InventoryManager>();
        if (inventory)
        {
            //check for item
            itemSO = inventory.GetActiveItem();
            if (itemSO && itemsSO.Count != 0 && itemsSO.Contains(itemSO)) 
            {
                UseItem(interactor);
            }
            else //show message if not item
            {
                Debug.Log("NO ITEM");
                if(showAlternativeMessage && showMessageSO) showMessageSO.ShowMessage(messageText);
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

    public void SetForceAlternativeMessage(bool force) 
    {
        forceAlternativeMessage = force;
    }
    public void SetMessageText(string text) 
    {
        messageText = text;
    }
    public string GetMessageText()
    {
        return messageText;
    }

}
