using Assets.Scripts.Interactions;
using UnityEngine;

public class PickUpInteract : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ItemSO itemSO;
    public void Interact(GameObject interactor)
    {
        InventoryManager inventory = interactor.gameObject.GetComponentInChildren<InventoryManager>();
        if (inventory)
        {
            inventory.AddItem(itemSO);
            Destroy(gameObject);
        }
    }
}