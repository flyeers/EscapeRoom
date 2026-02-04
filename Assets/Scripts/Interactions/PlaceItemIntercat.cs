using System;
using UnityEngine;
using UnityEngine.UI;

public class PlaceItemIntercat : NeedItemInteract
{
    [Header("Place item params")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private bool removePickUp = true;

    protected override void UseItem() 
    {
        if (deactivateAfterAction)//deactivate place on game object
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }

        GameObject obj = Instantiate(itemSO.ItemPrefab, spawnPoint.position, spawnPoint.rotation);
            
        if(removePickUp) //remove pick up on placed obejct
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
