using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : Puzzle
{
    [Header("Keypad")]
    [SerializeField] private string combination;

    [Header("Keypad info")]
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
    [SerializeField] private Color initialColor;
    [SerializeField] private Color errorColor;
    [SerializeField] private Color unlockedColor;
    [SerializeField] private float errorInfoTime = 1f;


    private string currentCombination = "";
    private string emptyCombination = "";
    private bool blocked = false;

    private void Start()
    {
        for (int i = 0; i < combination.Length; i++) 
        {
            emptyCombination += "*";
        }
        text.text = emptyCombination;
        image.color = initialColor;
    }

    private bool CheckCombination()
    {
        if (combination == currentCombination) return true;
        else return false;
    }

    public void KeyEntered(string key) 
    {
        if (!blocked) //if shoing info no input accepted
        {
            currentCombination += key;
            text.text = currentCombination;
            Debug.Log(currentCombination);


            if (currentCombination.Length == combination.Length)
            {
                if (locked && CheckCombination())
                {
                    blocked = true;
                    image.color = unlockedColor;

                    locked = false;
                    Debug.Log("Keypad Unlocked");

                    if (actionOnUnlocked) actionOnUnlocked.ExecuteAction();
                }
                else
                {
                    StartCoroutine(ErrorInfo());
                }
            }
        }
    }

    IEnumerator ErrorInfo()
    {
        blocked = true;
        image.color = errorColor;

        yield return new WaitForSeconds(errorInfoTime);

        image.color = initialColor;
        blocked = false;
        currentCombination = "";
        text.text = emptyCombination;
    }

}
