using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [Header("Range of motion")]
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    Vector3 startingPosition;

    float movementFactor;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        ObjectMove();
    }

    void ObjectMove()
    {
        if (period == Mathf.Epsilon) { return; }

        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;

        transform.position = startingPosition + offset;
    }
}
