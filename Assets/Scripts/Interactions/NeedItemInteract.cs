using Assets.Scripts.Interactions;
using NUnit.Framework.Interfaces;
using UnityEngine;

public class NeedItemInteract: MonoBehaviour, IInteractable
{
    [SerializeField] protected ItemSO itemSO;
    [SerializeField] protected Action action;
    [SerializeField] protected bool deactivateAfterAction = false;

    [Header("Alternative message")]
    [SerializeField] private bool showAlternativeMessage = true;
    [SerializeField] private ShowMessageSO showMessageSO;
    public string messageText = "";


    public void Interact(GameObject interactor)
    {
        InventoryManager inventory = interactor.gameObject.GetComponentInChildren<InventoryManager>();
        if (inventory)
        {
            if (inventory.CheckForItem(itemSO)) 
            {
                if(itemSO.consumable) inventory.RemoveItem(itemSO); //if consumable remove from inventory
                UseItem();
            }
            else 
            {
                Debug.Log("NO ITEM");
                if(showMessageSO) showMessageSO.ShowMessage(messageText);
            }
        }
    }

    protected virtual void UseItem() 
    {
        if (action) action.ExecuteAction(null);
        Debug.Log("OPEEEN");

        if (deactivateAfterAction)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
