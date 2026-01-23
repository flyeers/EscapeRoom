using Assets.Scripts.Interactions.Clickable;
using UnityEngine;

public class PickUpClick : MonoBehaviour, IClickable
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
