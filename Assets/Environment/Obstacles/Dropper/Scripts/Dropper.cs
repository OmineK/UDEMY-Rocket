using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    Rigidbody bodyDropper;
    bool playerCollision = false;

    void Awake()
    {
        bodyDropper = GetComponent<Rigidbody>();
    }

    void Start()
    {
        bodyDropper.useGravity = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bodyDropper.useGravity = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerCollision = true;

        if (!playerCollision)
            Destroy(this.gameObject);
    }
}
