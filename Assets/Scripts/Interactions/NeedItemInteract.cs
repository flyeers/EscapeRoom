using Assets.Scripts.Interactions;
using NUnit.Framework.Interfaces;
using UnityEngine;

public class NeedItemInteract: MonoBehaviour, IInteractable
{
    [SerializeField] private ItemSO itemSo;
    [SerializeField] private Action action;
    [SerializeField] private bool deactivateAfterAction = false;

    [Header("Alternative message")]
    [SerializeField] private bool showAlternativeMessage = true;
    [SerializeField] private ShowMessageSO showMessageSO;
    public string messageText = "";


    public void Interact(GameObject interactor)
    {
        InventoryManager inventory = interactor.gameObject.GetComponentInChildren<InventoryManager>();
        if (inventory)
        {
            if (inventory.CheckForItem(itemSo)) 
            {
                if(itemSo.consumable) inventory.RemoveItem(itemSo); //if consumable remove from inventory
                if (action) action.ExecuteAction();
                Debug.Log("OPEEEN");

                if (deactivateAfterAction) { 
                    gameObject.layer = LayerMask.NameToLayer("Default");
                }
            }
            else 
            {
                Debug.Log("NO KEYY");
                showMessageSO.ShowMessage(messageText);
            }
        }
    }
}
