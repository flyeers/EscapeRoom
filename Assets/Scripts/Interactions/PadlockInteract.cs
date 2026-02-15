using Assets.Scripts.Interactions;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEngine.Rendering.DebugUI.Table;

public class PadlockInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private Padlock padlock;
    [SerializeField] private int positionInPadlock;
    [Range(0, 9)]
    [SerializeField] private int maxPadlockNumber = 9;
    [SerializeField] bool clockwiseRotation = true;
    [SerializeField] Axis axis;

    private int number;
    private Vector3 rotationToAdd = new Vector3();

    public void Start()
    {
        int amount = (360 / (maxPadlockNumber + 1)) * (clockwiseRotation ? 1 : -1);//number to rotate + direction
        //Axis
        if (axis == Axis.X) { rotationToAdd = new Vector3(amount, 0f, 0f); }
        else if (axis == Axis.Z) { rotationToAdd = new Vector3(0f, 0f, amount); }
        else { rotationToAdd = new Vector3(0f, amount, 0f); }


        number = padlock.GetNumber(positionInPadlock);
        transform.Rotate(number * rotationToAdd);

    }

    public void Interact(GameObject interactor)
    {
        transform.Rotate(rotationToAdd);

        number = number + 1 > maxPadlockNumber ? 0 : number + 1;
        padlock.SetNumber(positionInPadlock, number);
    }

    public void SetUpWheel(Padlock padlock, int positionInPadlock, int maxPadlockNumber, bool clockwiseRotation, Axis axis) 
    { 
        this.padlock = padlock;
        this.positionInPadlock = positionInPadlock;
        this.maxPadlockNumber = maxPadlockNumber;
        this.clockwiseRotation = clockwiseRotation;
        this.axis = axis;
    }
}
