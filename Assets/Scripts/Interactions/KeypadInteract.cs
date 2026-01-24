using Assets.Scripts.Interactions;
using UnityEngine;

public class KeypadInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private Keypad keypad;
    [SerializeField] private string key;

    public void Interact(GameObject interactor)
    {
        keypad.KeyEntered(key);
    }
}
