using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    // reference variables
    [SerializeField] private ConfigurableJoint holdJoint;
    private Rigidbody objectRB;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private ParticleSystem breakEffect;
    [SerializeField] private DoorControl doorScript;

    // pickup variables
    private bool isHeld;
    private bool canDrop;
    private bool canGrab;

    // breaking variables
    public float breakSpeed;
    private float lastSpeed;

    // Start is called before the first frame update
    void Start()
    {
        objectRB = GetComponent<Rigidbody>();
    }

    // FixedUpdate is called once per physics loop.
    void FixedUpdate()
    {
        if (isHeld)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // canDrop stops the player picking up and dropping the item with the same click
                if (canDrop)
                {
                    objectRB.useGravity = true;
                    holdJoint.connectedBody = null;
                    isHeld = false;
                    canGrab = false;
                }
            }
            else
            {
                canDrop = true;
            }
        }
        // save speed because velocity is reduced by the time OnCollisionEnter is called
        lastSpeed = objectRB.velocity.magnitude;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Holder"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                // canGrab stops the player dropping and picking up the item again with the same click
                if (!isHeld && canGrab)
                {
                    objectRB.useGravity = false;
                    holdJoint.connectedBody = objectRB;
                    isHeld = true;
                    canDrop = false;
                }
            }
            else
            {
                canGrab = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(lastSpeed);
        // destroy object if it was moving above a set speed.
        // creates particle effect and opens door
        if (!isHeld && lastSpeed > breakSpeed)
        {
            Instantiate(breakEffect, transform.position, Quaternion.identity, null);
            Destroy(gameObject);
            doorAnimator.SetBool("Open", true);
            doorScript.canShut = false;
        }
    }
}
