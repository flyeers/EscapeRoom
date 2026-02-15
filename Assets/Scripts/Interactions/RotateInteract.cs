using Assets.Scripts.Interactions;
using System.Collections;
using UnityEngine;

public class RotateInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private Action action;
    [SerializeField] private bool deactivateAfterAction = false;

    [SerializeField] private GameObject objctToRotate;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float startAlngel = 0f;  
    [SerializeField] private float aimAngle = -45f;

    private bool isActivated = true;
    private bool isMoving = false;



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

        float targetAngle = isActivated ? aimAngle : startAlngel;

        Quaternion startRotation = objctToRotate.transform.localRotation;
        Quaternion targetRotation = Quaternion.Euler(targetAngle, 0, 0);

        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime * rotationSpeed;
            objctToRotate.transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, time);
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
