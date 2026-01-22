using Assets.Scripts.Interactions.Clickable;
using UnityEngine;

public class PadlockClick : MonoBehaviour, IClickable
{
    [SerializeField] private Padlock padlock;
    [SerializeField] private int positionInPadlock;

    private int number;

    public void Awake()
    {
        number = padlock.GetNumber(positionInPadlock);
        //SETEAR EL VISUAL PERO BUENO
    }

    public void Interact(GameObject interactor)
    {
        //SUMAMOS 1 de momento y asumimos q va del 0 al 9 

        number = number + 1 > 9 ? 0 : number + 1;
        padlock.SetNumber(positionInPadlock, number);
    }
}
