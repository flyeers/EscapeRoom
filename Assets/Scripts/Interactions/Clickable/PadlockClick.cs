using Assets.Scripts.Interactions.Clickable;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class PadlockClick : MonoBehaviour, IClickable
{
    [SerializeField] private Padlock padlock;
    [SerializeField] private int positionInPadlock;
    [Range(0, 9)]
    [SerializeField] private int maxPadlockNumber = 9;

    [SerializeField] private Vector3 rotationToAdd = new Vector3(0f, 36f, 0f);//y

    private int number;

    public void Start()
    {
        number = padlock.GetNumber(positionInPadlock);
        transform.Rotate(number * rotationToAdd);

    }

    public void Interact(GameObject interactor)
    {
        transform.Rotate(rotationToAdd);

        number = number + 1 > maxPadlockNumber ? 0 : number + 1;
        padlock.SetNumber(positionInPadlock, number);
    }
}
