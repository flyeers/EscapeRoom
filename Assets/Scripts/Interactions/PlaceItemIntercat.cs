using System;
using UnityEngine;
using UnityEngine.UI;

public class PlaceItemIntercat : NeedItemInteract
{
    [Header("Place item params")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private bool removePickUp = true;
    [SerializeField] private bool replacezableItem = false;

    protected override void UseItem(GameObject interactor) 
    {
        if (deactivateAfterAction && !replacezableItem)//deactivate place on game object
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }

        if (spawnPoint.childCount > 0) //An object was allready placed
        {
            
            ///////////not replazable but item is placed
            if (!replacezableItem) //re add item
            {
                inventory.AddItem(itemSO);
                return;
            } 
            ////////////

            if (spawnPoint.GetChild(0).gameObject.TryGetComponent<PickUpInteract>(out PickUpInteract pickUp)) 
            { 
                inventory.AddItem(pickUp.itemSO);
                Destroy(spawnPoint.GetChild(0).gameObject);
            }
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
