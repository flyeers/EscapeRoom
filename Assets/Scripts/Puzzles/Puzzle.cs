using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [Header("On unlocked")]
    [SerializeField] protected Action actionOnUnlocked;

    protected bool locked = true;

    public bool GetLocked() {  return locked; }

}
