using UnityEngine;
using UnityEngine.Animations;


public class SetUpPadlockWheelAction : Action
{
    [SerializeField] private Padlock padlock;
    [SerializeField] private int positionInPadlock;
    [Range(0, 9)]
    [SerializeField] private int maxPadlockNumber = 9;
    [SerializeField] bool clockwiseRotation = true;
    [SerializeField] Axis axis;

    [SerializeField] private string newLayerName;

    public override void ExecuteAction(GameObject obejct) 
    {
        if (!obejct) return;

        PadlockInteract padlockInteract = obejct.AddComponent<PadlockInteract>();
        if (padlockInteract) 
        { 
            padlockInteract.SetUpWheel(padlock, positionInPadlock, maxPadlockNumber, clockwiseRotation, axis);
            obejct.layer = LayerMask.NameToLayer(newLayerName);
        }
    }
}
