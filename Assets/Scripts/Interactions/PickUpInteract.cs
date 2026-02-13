using Assets.Scripts.Interactions;
using UnityEngine;

public class PickUpInteract : MonoBehaviour, IInteractable
{
    [SerializeField]
    public ItemSO itemSO;
    public void Interact(GameObject interactor)
    {
        InventoryManager inventory = interactor.gameObject.GetComponentInChildren<InventoryManager>();
        if (inventory)
        {
            if (inventory.AddItem(itemSO)) 
            {
                Destroy(gameObject);
            }
        }
    }
}