using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class mainMenu : MonoBehaviour
{

    public GameObject SettingsMenu;

    [SerializeField]
    public GameObject[] menubuttons;

    void VersusSelected() 
    {

        // play sound effect, play animation, then load versus scene

        // hmmmmm sfx and animation can probably be in their own reusable function once im putting those in

    }

    void StorySelected() 
    {

        // popup that tells you its not ready yet

    }

    void SettingsSelected() 
    {

        // load settings menu

    }

    void ReturnToTitle() {

        // play sound effect, play animation, then load title menu

        SceneManager.LoadScene("startMenu");

    }
}
