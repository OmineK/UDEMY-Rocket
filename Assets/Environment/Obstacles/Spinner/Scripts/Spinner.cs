using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [Header("Spinner rotate speed/direction")]
    [SerializeField] [Range (1f, 10f)] float rotateSpeed = 10f;
    [SerializeField] bool rightDirection = false;
    [SerializeField] bool leftDirection = false;

    void Update()
    {
        SpinDirection();
    }

    void SpinDirection()
    {
        if (rightDirection)
        {
            leftDirection = false;
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime * -10f);  
        }

        else if (leftDirection)
        {
            rightDirection = false;
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime * 10f);
        }
    }
}
