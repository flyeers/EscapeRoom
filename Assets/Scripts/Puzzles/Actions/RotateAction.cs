using System.Collections;
using UnityEngine;

public class RotateAction : Action
{
    [SerializeField] private Action nextAction;


    [SerializeField] private GameObject objctToRotate;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private Vector3 rotationToAdd;
    [SerializeField] private bool deactivateAfterAction = false;


    private bool isActivated = true;
    private bool isMoving = false;

    private Vector3 initialRotation;

    private void Awake()
    {
        initialRotation = objctToRotate.transform.localEulerAngles;
    }

    public override void ExecuteAction(GameObject obejct)
    {
        if (!isMoving)
        {
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        isMoving = true;

        Vector3 startRotation = objctToRotate.transform.localEulerAngles;
        Vector3 targetRotation;

        if (isActivated)
            targetRotation = initialRotation + rotationToAdd;
        else
            targetRotation = initialRotation;

        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime * rotationSpeed;

            Vector3 currentRotation = Vector3.Lerp(startRotation, targetRotation, time);
            objctToRotate.transform.localEulerAngles = currentRotation;

            yield return null;
        }

        objctToRotate.transform.localEulerAngles = targetRotation;

        isActivated = !isActivated;
        isMoving = false;

        if (nextAction) nextAction.ExecuteAction(null);
        if (deactivateAfterAction)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}