using Unity.Cinemachine;
using UnityEngine;

public class MoveAction : Action
{
    [Header("Object to move")]
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private Vector3 movementRange = new Vector3();

    [Header("Camera")]
    [SerializeField] private CinemachineCamera newCamera;
    [SerializeField] private CloseUpInteract closeUpInteract;

    public override void ExecuteAction()
    {
        if (objectToMove) objectToMove.transform.position += movementRange;
        if (closeUpInteract && newCamera) 
        {
            closeUpInteract.GetVirtualCamera().enabled = false;
            newCamera.enabled = true;
            closeUpInteract.SetVirtualCamera(newCamera);
        }
        
    }

}
