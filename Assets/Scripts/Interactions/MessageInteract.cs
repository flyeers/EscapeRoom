using Assets.Scripts.Interactions;
using TMPro;
using UnityEngine;

public class MessageInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private ShowMessageSO showMessageSO;

    [TextArea(3, 10)]
    public string messageText = "";

    public void Interact(GameObject interactor)
    {
        if (showMessageSO.ShowMessage(messageText)) 
        {
            //Remove outline
            if (gameObject.TryGetComponent<Outline>(out Outline otline)) otline.enabled = false;
        }
    }

}