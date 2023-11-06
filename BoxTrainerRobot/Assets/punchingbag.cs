using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class PunchingBag : MonoBehaviour
    {
        public GameObject Righthand; // Make sure this is assigned in the Inspector.

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("Trigger Entered"); // Add a debug statement to check if the trigger is entered.

            if (other.gameObject == Righthand)
            {
                // Handle the punch detection for the Right Hand.
                Debug.Log("Right punch detected.");
            }
            else
            {
                Debug.Log("Object entered the trigger, but it's not the right hand.");
            }
        }
    }
