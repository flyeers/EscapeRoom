using Assets.Scripts.Interactions;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyInteract : MonoBehaviour, IInteractable
{
    public void Interact(GameObject interactor)
    {
        gameObject.SetActive(false);
    }
}
