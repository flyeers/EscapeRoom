using Assets.Scripts.Interactions.Clickable;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyClick : MonoBehaviour, IClickable
{
    public void Interact(GameObject interactor)
    {
        gameObject.SetActive(false);
    }
}
