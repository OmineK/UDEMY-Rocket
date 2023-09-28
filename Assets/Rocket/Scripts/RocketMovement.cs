using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketMovement : MonoBehaviour
{
    [Header("Movement speed")]
    [SerializeField] float yAxisMoveSpeed = 1000f;
    [SerializeField] float zAxisRotateSpeed = 250f;

    [Header("Audio clips")]
    [SerializeField] AudioClip flyingClip;

    [Header("Particles")]
    [SerializeField] ParticleSystem mainBoosterParticle;
    [SerializeField] ParticleSystem leftBoosterParticle;
    [SerializeField] ParticleSystem rightBoosterParticle;

    Rigidbody bodyRocket;
    AudioSource rocketSound;

    void Start()
    {
        bodyRocket = GetComponent<Rigidbody>();
        rocketSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopTrusting();
        }
    }

    void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            ApproveLeftRotate();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            ApproveRightRotate();
        }
        else
        {
            StopRotate();
        }
    }

    void StartThrusting()
    {
        bodyRocket.AddRelativeForce(Vector3.up * yAxisMoveSpeed * Time.deltaTime);

        if (!mainBoosterParticle.isPlaying)
        {
            mainBoosterParticle.Play();
        }

        if (!rocketSound.isPlaying)
        {
            rocketSound.PlayOneShot(flyingClip);
        }
    }

    void StopTrusting()
    {
        rocketSound.Stop();
        mainBoosterParticle.Stop();
    }

    void ApproveLeftRotate()
    {
        if (!rightBoosterParticle.isPlaying)
        {
            rightBoosterParticle.Play();
        }

        ApplyRotation(zAxisRotateSpeed);
    }

    void ApproveRightRotate()
    {
        if (!leftBoosterParticle.isPlaying)
        {
            leftBoosterParticle.Play();
        }

        ApplyRotation(-zAxisRotateSpeed);
    }

    void StopRotate()
    {
        leftBoosterParticle.Stop();
        rightBoosterParticle.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        bodyRocket.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        bodyRocket.freezeRotation = false;
    }
}