using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    public GameObject leftHand; // Reference to the left hand game object.
    public GameObject rightHand; // Reference to the right hand game object.

    private List<string> combos = new List<string>
    {
        "LR", // Left punch followed by right punch.
        "LLL", // Three left punches in a row.
        "RR",  // Right punch followed by right punch.
        "LRLR" // Left, right, left, right
    };

    private int currentComboIndex = 0;
    private string currentInput = "";
    private float lastPunchTime = 0.0f;
    private float punchCooldown = 0.5f;
    public Text comboText;
    private bool isObservable = true;
    private float observableDuration = 5.0f;
    private float observableDelay = 0.3f; // Delay before activating observable state.
    public GameObject playerPosition;

    public GameObject robot;

    void Start()
    {
        if (combos.Count > 0)
        {
            UpdateComboText("Perform Combo: " + combos[0]);
        }

        StartCoroutine(ObserveState());
    }

    IEnumerator ObserveState()
    {
        if (currentComboIndex > 0) // Only add the delay after the first combo.
        {
            yield return new WaitForSeconds(observableDelay);
        }

        // Deactivate the left and right hand game objects during the observable state.
        leftHand.SetActive(false);
        rightHand.SetActive(false);

        // Play the animation for the current combo.
        string animationName = GetComboAnimationName(currentComboIndex + 1);
        robot.GetComponent<Animator>().Play(animationName);

        Vector3 originalPosition = playerPosition.transform.position;
        playerPosition.transform.position = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z + 2.0f);

        yield return new WaitForSeconds(observableDuration);

        // Activate the left and right hand game objects after the observable state.
        leftHand.SetActive(true);
        rightHand.SetActive(true);

        isObservable = false;
        playerPosition.transform.position = originalPosition;
        UpdateComboText("Perform Combo: " + combos[currentComboIndex]);
    }

    string GetComboAnimationName(int comboNumber)
    {
        return "Combo" + comboNumber.ToString();
    }

    void Update()
    {
        if (isObservable)
        {
            // Disable input during the observable state.
            currentInput = ""; // Clear the input so no combo is registered.
            return;
        }

        if (Time.time - lastPunchTime >= punchCooldown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentInput += "L";
                lastPunchTime = Time.time;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                currentInput += "R";
                lastPunchTime = Time.time;
            }
        }

        if (currentComboIndex < combos.Count)
        {
            string currentCombo = combos[currentComboIndex];

            if (currentInput == currentCombo)
            {
                UpdateComboText("Combo " + (currentComboIndex + 1) + " successful: " + currentCombo);
                currentComboIndex++;

                if (currentComboIndex < combos.Count)
                {
                    UpdateComboText("Perform Combo: " + combos[currentComboIndex]);
                }
                else
                {
                    // All combos executed, end training session.
                    StartCoroutine(EndTrainingSession());
                }

                currentInput = "";

                // Activate the observable state after each combo.
                StartCoroutine(ObserveState());
            }
            else if (currentInput.Length >= currentCombo.Length)
            {
                UpdateComboText("Combo " + (currentComboIndex + 1) + " failed. Try again: " + currentCombo);
                currentInput = "";
            }
        }
    }

    IEnumerator EndTrainingSession()
    {
        yield return new WaitForSeconds(2.0f);

        // Display end message.
        UpdateComboText("Training session done");

        // Block mechanics or perform any other end-of-session actions.
    }

    void UpdateComboText(string text)
    {
        comboText.text = text;
    }
}