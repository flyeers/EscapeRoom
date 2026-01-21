using Assets.Scripts.Interactions;
using UnityEngine;

public class CloseUpInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private Camera camera;

    public void Interact(GameObject interactor)
    {
        if (camera) 
        {
            Interactor interact = interactor.GetComponent<Interactor>();
            if (interact)
            {
                interact.HandleInteractionInfo(false);
            }

            ControllerManager manager = interactor.GetComponent<ControllerManager>();
            if (manager) 
            {
                manager.ChangeControllers(false, camera);
            }
    
        }  
    }
}
