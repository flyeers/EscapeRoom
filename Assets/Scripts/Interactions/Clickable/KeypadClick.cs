using Assets.Scripts.Interactions.Clickable;
using UnityEngine;

public class KeypadClick : MonoBehaviour, IClickable
{
    [SerializeField] private Keypad keypad;
    [SerializeField] private string key;

    public void Interact(GameObject interactor)
    {
        keypad.KeyEntered(key);
    }
}
