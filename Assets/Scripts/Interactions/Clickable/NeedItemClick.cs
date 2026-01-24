using Assets.Scripts.Interactions.Clickable;
using NUnit.Framework.Interfaces;
using UnityEngine;

public class NeedItemClick : MonoBehaviour, IClickable
{
    [SerializeField] private ItemSO itemSo;
    [SerializeField] private Action action;


    public void Interact(GameObject interactor)
    {
        InventoryManager inventory = interactor.gameObject.GetComponentInChildren<InventoryManager>();
        if (inventory)
        {
            if (inventory.CheckForItem(itemSo)) 
            {
                if(itemSo.consumable) inventory.RemoveItem(itemSo); //if consumable remove from inventory
                if (action) action.ExecuteAction();

                //TODO ¿?¿?Eliminar componente ¿??  - no se puede repetir la accion 
                Debug.Log("OPEEEN");
            }
            else 
            {
                Debug.Log("NO KEYY");
            }
        }
    }
}
