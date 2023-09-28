using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinish : MonoBehaviour
{
    TextMeshProUGUI finishText;
    bool gameFinish = false;


    void Awake()
    {
        finishText = FindObjectOfType<TextMeshProUGUI>();
    }

    void Start()
    {
        finishText.enabled = false;
    }

    void Update()
    {
        ReloadGame();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            finishText.enabled = true;
            gameFinish = true;
        }
    }

    void ReloadGame()
    {
        if (gameFinish)
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(2); //first scene w/o introduction
            }
        }
    }
}
