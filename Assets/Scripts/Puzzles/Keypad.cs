using System;
using TMPro;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    [Header("Padlock")]
    [SerializeField] private string combination;
    [SerializeField] private TextMeshProUGUI text;

    [Header("On unlocked")]
    [SerializeField] private Action actionOnUnlocked;

    private string currentCombination = "";
    private string emptyCombination = "";
    private bool locked = true;

    private void Start()
    {
        for (int i = 0; i < combination.Length; i++) 
        {
            emptyCombination += "*";
        }
        text.text = emptyCombination;

    }

    private bool CheckCombination()
    {
        if (combination == currentCombination) return true;
        else return false;
    }

    public void KeyEntered(string key) 
    {
        currentCombination += key;
        text.text = currentCombination;
        Debug.Log(currentCombination);


        if (currentCombination.Length == combination.Length)
        {
            if (locked && CheckCombination()) 
            {
                locked = false;
                Debug.Log("Keypad Unlocked");

                if (actionOnUnlocked) actionOnUnlocked.ExecuteAction();
            }
            else 
            {
                currentCombination = "";
                text.text = emptyCombination;
            }
        }
        
    }

}
