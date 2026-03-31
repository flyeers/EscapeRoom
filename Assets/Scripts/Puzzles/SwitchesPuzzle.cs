using UnityEngine;

public class SwitchesPuzzle : Puzzle
{
    [SerializeField] private Light[] lights;

    private bool CheckCombination()
    {
        foreach (var light in lights) //all lights on 
        { 
            if (!light.enabled) return false;
        }
        return true;
    }

    public void NotifyChange() 
    {
        if (locked && CheckCombination()) 
        {
            locked = false;
            Debug.Log("UNLOCKED");
            if (actionOnUnlocked != null) actionOnUnlocked.ExecuteAction(null);
        }
    }
}
