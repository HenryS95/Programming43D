using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{

    [SerializeField] private Transform Spawnpoint;
    [SerializeField] private Rigidbody Prefab;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
           {
                Rigidbody RigidPrefab;
               RigidPrefab = Instantiate(Prefab, Spawnpoint.position, Spawnpoint.rotation) as Rigidbody;
            }
    }
}