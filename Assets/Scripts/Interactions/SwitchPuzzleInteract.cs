using Assets.Scripts.Interactions;
using UnityEngine;

public class SwitchPuzzleInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private Light[] lightsOn;
    [SerializeField] private Light[] lightsOff;
    [SerializeField] private SwitchesPuzzle switchesPuzzle;

    public void Interact(GameObject interactor)
    {
        foreach (Light light in lightsOn) 
        {
            light.enabled = true;
        }
        foreach (Light light in lightsOff)
        {
            light.enabled = false;
        }

        if(switchesPuzzle) switchesPuzzle.NotifyChange();
    }
}
