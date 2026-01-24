using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class MoveAction : Action
{
    [Header("Object to move")]
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private Vector3 movementRange = new Vector3();
    [SerializeField] private float duration = 1f;


    [Header("Camera")]
    [SerializeField] private CinemachineCamera newCamera;
    [SerializeField] private CloseUpInteract closeUpInteract;

    public override void ExecuteAction()
    {
        //objectToMove.transform.position += movementRange;

        if (objectToMove) StartCoroutine(Move());

        if (closeUpInteract && newCamera) 
        {
            closeUpInteract.GetVirtualCamera().enabled = false;
            newCamera.enabled = true;
            closeUpInteract.SetVirtualCamera(newCamera);
        }
        
    }

    IEnumerator Move()
    {
        Vector3 startPos = objectToMove.transform.position;
        Vector3 endPos = startPos + movementRange;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            objectToMove.transform.position = Vector3.Lerp(startPos, endPos, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        objectToMove.transform.position = endPos;
    }

}
