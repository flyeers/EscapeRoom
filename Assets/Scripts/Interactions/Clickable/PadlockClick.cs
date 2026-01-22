using Assets.Scripts.Interactions.Clickable;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class PadlockClick : MonoBehaviour, IClickable
{
    [SerializeField] private Padlock padlock;
    [SerializeField] private int positionInPadlock;

    [SerializeField] private Vector3 rotationToAdd = new Vector3(0f, 36f, 0f);//y

    private int number;

    public void Start()
    {
        number = padlock.GetNumber(positionInPadlock);
        transform.Rotate(number * rotationToAdd);

    }

    public void Interact(GameObject interactor)
    {
        //SUMAMOS 1 de momento y asumimos q va del 0 al 9 

        transform.Rotate(rotationToAdd);

        number = number + 1 > 9 ? 0 : number + 1;
        padlock.SetNumber(positionInPadlock, number);
    }
}
