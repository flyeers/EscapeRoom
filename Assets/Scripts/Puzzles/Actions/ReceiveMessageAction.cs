using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ReceiveMessageAction : Action
{
    [Header("Message info")]
    [SerializeField] private ItemSO itemToUpdateMessage;
    [SerializeField] private string newMessage = "New message";

    [Header("Notify info")]
    [SerializeField] private bool notify = false;
    [SerializeField] private float delayToUpdate = 0;

    private InventoryManager inventory;

    public override void ExecuteAction(GameObject obejct)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player) return;

        if (player.TryGetComponent<InventoryManager>(out inventory))
        {
            if (inventory.CheckForItem(itemToUpdateMessage)) 
            {
                if(!notify) 
                {
                    //set new message
                    itemToUpdateMessage.message = newMessage;
                    inventory.RefreshMessageUI(itemToUpdateMessage);
                }
                else 
                {
                    //set new message + notify
                    StartCoroutine(DelayToUpdate());
                }
            }
        }

    }
    IEnumerator DelayToUpdate()
    {
        yield return new WaitForSeconds(delayToUpdate);
        itemToUpdateMessage.message = newMessage;
        inventory.RefreshMessageUI(itemToUpdateMessage);
        inventory.NotifyMessageUI(itemToUpdateMessage);
    }


}
