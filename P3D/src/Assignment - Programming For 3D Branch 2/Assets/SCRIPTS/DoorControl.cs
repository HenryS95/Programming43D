using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    // reference variables
    [SerializeField] private Animator doorAnimator;

    // edge case variable
    public bool canShut;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // checks which side of the door the player is on, so the door only opens from one side
            // switch the '<' sign to swap sides
            Vector3 toOther = other.transform.position - transform.position;
            if (Vector3.Dot(toOther, transform.forward) < 0f)
            {
                doorAnimator.SetBool("Open", true);
            }
        }
    }

    // Shuts door whenever the player leaves the openning area.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // checks player side again. Will only swap canShut to true if player is 'outside'
            // this prevents player trapping themselves by going near the door after using the key
            // canShut is normally true and only set to false when the key object is smashed
            Vector3 toOther = other.transform.position - transform.position;
            if (Vector3.Dot(toOther, transform.forward) < 0f)
            {
                canShut = true;
            }
            if (canShut)
            {
                doorAnimator.SetBool("Open", false);
            }
        }
    }
}
