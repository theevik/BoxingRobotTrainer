using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PunchingBag : MonoBehaviour
{
    public GameObject Righthand; // Assign the right hand GameObject in the Inspector.
    public GameObject Lefthand; // Assign the left hand GameObject in the Inspector.
    public float punchCooldown = 1.0f; // Adjust the cooldown duration as needed.
    private float lastPunchTime = 0.0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Righthand || other.gameObject == Lefthand)
        {
            if (Time.time - lastPunchTime >= punchCooldown)
            {
                if (other.gameObject == Righthand)
                {
                    // Handle the punch detection for the Right Hand.
                    Debug.Log("Right trigger");
                }
                else if (other.gameObject == Lefthand)
                {
                    // Handle the punch detection for the Left Hand.
                    Debug.Log("Left trigger");
                }

                // Update the last punch time.
                lastPunchTime = Time.time;
            }
        }
    }
}