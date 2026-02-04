using UnityEngine;

public class SetUpPadlockWheelAction : Action
{
    [SerializeField] private Padlock padlock;
    [SerializeField] private int positionInPadlock;
    [Range(0, 9)]
    [SerializeField] private int maxPadlockNumber = 9;
    [SerializeField] private Vector3 rotationToAdd = new Vector3(0f, 36f, 0f);

    [SerializeField] private string newLayerName;

    public override void ExecuteAction(GameObject obejct) 
    {
        if (!obejct) return;

        PadlockInteract padlockInteract = obejct.AddComponent<PadlockInteract>();
        if (padlockInteract) 
        { 
            padlockInteract.SetUpWheel(padlock, positionInPadlock, maxPadlockNumber, rotationToAdd);
            obejct.layer = LayerMask.NameToLayer(newLayerName);
        }
    }
}
