using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderDetector : MonoBehaviour
{
    [Header("Scene reload delay")]
    [SerializeField] float delayReload = 2f;

    [Header("Audio clips")]
    [SerializeField] AudioClip explodeClip;
    [SerializeField] AudioClip successClip;

    [Header("Particles")]
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem finishParticleRight;
    [SerializeField] ParticleSystem finishParticleLeft;

    bool isTransitioning = false;
    bool finish = false;

    AudioSource playerSFX;
    RocketMovement movement;

    void Awake()
    {
        movement = GetComponent<RocketMovement>();
        playerSFX = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    break;
                case "Finish":
                    SuccessSequence();
                    break;
                default:
                    CrashSequence();
                    break;
            }
    }

    void SuccessSequence()
    {
        movement.enabled = false;

        if (!finish)
        {
            finishParticleRight.Play();
            finishParticleLeft.Play();

            if (!isTransitioning)
            {
                isTransitioning = true;
                playerSFX.Stop();
                playerSFX.PlayOneShot(successClip);
            }

            Invoke(nameof(LoadNextScene), delayReload);

            finish = true;
        }
    }

    void CrashSequence()
    {
        movement.enabled = false;

        if (!finish)
        {
            crashParticle.Play();

            if (!isTransitioning)
            {
                isTransitioning = true;
                playerSFX.Stop();
                playerSFX.PlayOneShot(explodeClip);
            }
            Invoke(nameof(ReloadScene), delayReload);

            finish = true;
        }
    }

    void ReloadScene()
    {
        isTransitioning = false;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextScene()
    {
            isTransitioning = false;
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex != SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextSceneIndex);
    }
}
