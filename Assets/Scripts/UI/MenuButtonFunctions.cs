using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonFunctions : MonoBehaviour
{
    public void resume()
    {
        //sets current menu to inactive and returns to play
        GameManager.instance.unpausedState();
    }

    public void restart()
    {
        //restarts game by reloading the active scene
        SceneManager.LoadScene("Prototype2");
        Currency.instance.ResetDrachma();
        GameManager.instance.unpausedState();
    }

    public void quit()
    {
        //closes out the game
        Application.Quit();
    }

    public void goToMainMenu()
    {
        GameManager.instance.unpausedState();
        Currency.instance.Cease();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
        Destroy(MazeState.instance);
    }
}
