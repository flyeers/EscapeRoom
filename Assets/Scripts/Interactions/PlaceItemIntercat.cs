using System;
using UnityEngine;
using UnityEngine.UI;

public class PlaceItemIntercat : NeedItemInteract
{
    [Header("Place item params")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private bool removePickUp = true;
    [SerializeField] private bool replaceableItem = false;

    protected override void UseItem(GameObject interactor) 
    {
        if (deactivateAfterAction && !replaceableItem)//deactivate place on game object
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }

        //An object was allready placed
        if (spawnPoint.childCount > 0) 
        {
            
            ////not replazable but item is placed - exit
            if (!replaceableItem) return;

            if (itemSO.consumable) inventory.RemoveItem();//remove from inventory
            if (spawnPoint.GetChild(0).gameObject.TryGetComponent<PickUpInteract>(out PickUpInteract pickUp)) 
            { 
                inventory.AddItem(pickUp.itemSO);
                Destroy(spawnPoint.GetChild(0).gameObject);
            }
        }
        else 
        {
            if (itemSO.consumable) inventory.RemoveItem();//remove from inventory
        }


        GameObject obj = Instantiate(itemSO.ItemPrefab, spawnPoint.position, spawnPoint.rotation);
        obj.transform.SetParent(spawnPoint);
        obj.layer = gameObject.layer; //set same layer as the gameobject where is been placeds

        if (removePickUp) //remove pick up on placed obejct
        {
            if (obj.TryGetComponent<PickUpInteract>(out PickUpInteract pickUpInteract))
            {
                Destroy(pickUpInteract);
                obj.layer = LayerMask.GetMask("Default");
            } 
        }

      
        if (action) action.ExecuteAction(obj);
        
    }
}
