using Assets.Scripts.Interactions;
using Unity.Cinemachine;
using UnityEngine;

public class CloseUpInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private CinemachineCamera camera;

    public void Interact(GameObject interactor)
    {
        if (camera) 
        {
            ControllerManager manager = interactor.GetComponent<ControllerManager>();
            if (manager) 
            {
                manager.ChangeControllers(false, camera);
            }
    
        }  
    }

    public void SetVirtualCamera(CinemachineCamera newCamera) 
    { 
        camera = newCamera;
    }
    public CinemachineCamera GetVirtualCamera()
    {
        return camera;
    }
}
