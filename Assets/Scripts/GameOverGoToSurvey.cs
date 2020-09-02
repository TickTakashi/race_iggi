using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverGoToSurvey : MonoBehaviour
{
    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("end reached!");
            SceneManager.LoadScene(1); // Transition to survey scene
        }
           
    }
}
