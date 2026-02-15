using Assets.Scripts.Interactions;
using UnityEngine;

public class KeypadInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private Keypad keypad;
    [SerializeField] private char key;

    public void Interact(GameObject interactor)
    {
        keypad.KeyEntered(key.ToString());
    }
}
