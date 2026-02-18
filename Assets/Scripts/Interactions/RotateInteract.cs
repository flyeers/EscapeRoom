using Assets.Scripts.Interactions;
using System.Collections;
using UnityEngine;

public class RotateInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private Action action;
    [SerializeField] private bool deactivateAfterAction = false;

    [SerializeField] private GameObject objctToRotate;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private Vector3 rotationToAdd;


    private bool isActivated = false;
    private bool isMoving = false;


    private Quaternion initialRotation;
    private Quaternion activatedRotation;

    private void Awake()
    {
        initialRotation = objctToRotate.transform.localRotation;
        activatedRotation = initialRotation * Quaternion.Euler(rotationToAdd);
    }

    public void Interact(GameObject interactor)
    {
        if (!isMoving)
        {
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        isMoving = true;

        Quaternion startRotation = objctToRotate.transform.localRotation;
        Quaternion targetRotation = isActivated ? initialRotation : activatedRotation;

        float time = 0;

        while (time < 1f)
        {
            time += Time.deltaTime * rotationSpeed;
            objctToRotate.transform.localRotation =
                Quaternion.Slerp(startRotation, targetRotation, time);
            yield return null;
        }

        objctToRotate.transform.localRotation = targetRotation;

        isActivated = !isActivated;
        isMoving = false;

        //Do action and/or deactivate
        if (action) action.ExecuteAction(null);
        if (deactivateAfterAction)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
