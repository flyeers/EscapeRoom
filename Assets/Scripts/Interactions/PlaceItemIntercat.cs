using System;
using UnityEngine;
using UnityEngine.UI;

public class PlaceItemIntercat : NeedItemInteract
{
    [Header("Place item params")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private bool deactivatePickUp = true;

    protected override void UseItem() 
    {
        GameObject obj = Instantiate(itemSO.ItemPrefab, spawnPoint.position, spawnPoint.rotation);
            
        if(deactivatePickUp) 
        {
            if (obj.TryGetComponent<PickUpInteract>(out PickUpInteract pickUpInteract))
            {
                pickUpInteract.enabled = false;
                obj.layer = LayerMask.GetMask("Default");
            } 
        }


        if (action) action.ExecuteActionObject(obj);
        if (deactivateAfterAction)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
