using UnityEngine;

public class TriggerExecuteAcition : MonoBehaviour
{
    [SerializeField] Action action;
    [SerializeField] bool deactivateAfterTrigger;

    private bool isEnabled = true;
    void OnTriggerEnter(Collider other)
    {
        if (isEnabled && other.CompareTag("Player"))
        {
            if (action) action.ExecuteAction(other.gameObject);
            if (deactivateAfterTrigger) isEnabled = false;
        }
    }
}
