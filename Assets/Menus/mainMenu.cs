using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class MainMenu : MonoBehaviour
{

    public GameObject SettingsMenu;

    [SerializeField]
    public GameObject[] menubuttons;

    public int MenuSelected;

    void Start() {
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            p.GetComponent<Player>().FindUIDirector();
        }
    }

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



    public void MenuCursorUp() {

        MenuSelected = Math.Abs((MenuSelected + 1) % menubuttons.Length);

        Debug.Log(MenuSelected.ToString());

    }

    public void MenuCursorDown() {

        MenuSelected = Math.Abs((MenuSelected - 1) % menubuttons.Length);

        Debug.Log(MenuSelected.ToString());

    }
    public void MenuCursorEnter() {

        switch (MenuSelected)
        {
            case 0: // versus

                Debug.Log("(guilty gear announcer voice) VERSUS");

                break;

            case 1: // story

                Debug.Log("(guilty gear announcer voice) STORY");

                break;

            case 2: //settinhs

                Debug.Log("(guilty gear announcer voice) CONFIG");

                break;

            case 3: // exit

                Debug.Log("wait i can actually do somethin here");

                ///ReturnToTitle();

                break;
        }

    }
    
        

}
