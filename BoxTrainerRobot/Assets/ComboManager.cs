using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    // Define the combo sequences here.
    private List<string> combos = new List<string>
    {
        "LR", // Left punch followed by right punch.
        "LLL", // Three left punches in a row.
        "RR",  // Right punch followed by right punch.
        "LLRL" // Left, left, right, left.
    };

    private int currentComboIndex = 0;
    private string currentInput = "";

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button for left punch.
        {
            currentInput += "L";
        }
        else if (Input.GetMouseButtonDown(1)) // Right mouse button for right punch.
        {
            currentInput += "R";
        }

        if (currentComboIndex < combos.Count)
        {
            string currentCombo = combos[currentComboIndex];

            if (currentInput == currentCombo)
            {
                Debug.Log("Combo " + (currentComboIndex + 1) + " successful: " + currentCombo);
                currentComboIndex++;

                if (currentComboIndex == combos.Count)
                {
                    Debug.Log("All combos completed!");
                    currentComboIndex = 0;
                }

                currentInput = "";
            }
        }
    }
}
