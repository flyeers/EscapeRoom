using UnityEngine;

public class MoveAction : Action
{
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private Vector3 movementRange = new Vector3();

    [SerializeField] private Camera camera;
    [SerializeField] private Vector3 addCameraPosition = new Vector3();
    [SerializeField] private Vector3 addCameraRotation = new Vector3();

    public override void ExecuteAction()
    {
        if (objectToMove) objectToMove.transform.position += movementRange;

        if (camera) 
        {
            camera.transform.position += addCameraPosition;
            camera.transform.Rotate(addCameraRotation);
        }
    }

}
