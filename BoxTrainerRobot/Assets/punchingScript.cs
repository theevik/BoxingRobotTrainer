using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class punchingScript : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    public float punchDistance = 1.0f;
    public float punchSpeed = 3.0f;
    public float punchCooldown = 1.0f; // Adjust the cooldown duration as needed.

    private Vector3 leftHandInitialPosition;
    private Vector3 rightHandInitialPosition;
    private bool canPunch = true;

    private void Start()
    {
        // Record the initial positions of the hands.
        leftHandInitialPosition = leftHand.transform.localPosition;
        rightHandInitialPosition = rightHand.transform.localPosition;
    }

    private void Update()
    {
        // Check for left mouse button click to trigger a left punch.
        if (Input.GetMouseButtonDown(0) && canPunch)
        {
            StartCoroutine(PunchLeft());
        }

        // Check for right mouse button click to trigger a right punch.
        if (Input.GetMouseButtonDown(1) && canPunch)
        {
            StartCoroutine(PunchRight());
        }
    }

    private IEnumerator PunchLeft()
    {
        canPunch = false; // Disable punching during the punch.

        Vector3 punchDirection = Vector3.forward;
        Vector3 targetPosition = leftHand.transform.localPosition + punchDirection * punchDistance;
        float journeyLength = Vector3.Distance(leftHand.transform.localPosition, targetPosition);
        float startTime = Time.time;

        while (leftHand.transform.localPosition != targetPosition)
        {
            float distanceCovered = (Time.time - startTime) * punchSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            leftHand.transform.localPosition = Vector3.Lerp(leftHand.transform.localPosition, targetPosition, fractionOfJourney);
            yield return null;
        }

        leftHand.transform.localPosition = leftHandInitialPosition; // Reset the hand's position.

        yield return new WaitForSeconds(punchCooldown); // Cooldown duration.
        canPunch = true; // Enable punching after cooldown.
    }

    private IEnumerator PunchRight()
    {
        canPunch = false; // Disable punching during the punch.

        Vector3 punchDirection = Vector3.forward;
        Vector3 targetPosition = rightHand.transform.localPosition + punchDirection * punchDistance;
        float journeyLength = Vector3.Distance(rightHand.transform.localPosition, targetPosition);
        float startTime = Time.time;

        while (rightHand.transform.localPosition != targetPosition)
        {
            float distanceCovered = (Time.time - startTime) * punchSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            rightHand.transform.localPosition = Vector3.Lerp(rightHand.transform.localPosition, targetPosition, fractionOfJourney);
            yield return null;
        }

        rightHand.transform.localPosition = rightHandInitialPosition; // Reset the hand's position.

        yield return new WaitForSeconds(punchCooldown); // Cooldown duration.
        canPunch = true; // Enable punching after cooldown.
    }
}