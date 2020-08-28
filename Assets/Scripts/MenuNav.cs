using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNav : MonoBehaviour
{

    // This method is attached to the start button, and when clicked, plays the next scene in the queue.
    // In Window > Build Settings, you can set the scene queue order. At the moment, there are only two scenes;
    // 0 for the menu, and 1 for the main game.
    public void playGame()
    {
      SceneManager.LoadScene(1);
    }

    public void playMayhem()
    {
      SceneManager.LoadScene(2);
    }

    // Quits the game. Note that this will not work in the Unity editor.
    public void quitGame()
    {
      Debug.Log("In real life, you just quit.");
      Application.Quit();
    }

    public void instructions()
    {
      SceneManager.LoadScene(3);
    }

    public void mainMenu()
    {
      SceneManager.LoadScene(0);
    }

}
