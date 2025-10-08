using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using JetBrains.Annotations;

public class MainMenu : MonoBehaviour
{

    public GameObject SettingsMenu;

    [SerializeField]
    public GameObject[] row1;

    [SerializeField]
    public GameObject[] row2;

    public bool RowSelected = false;
    public bool MenuSelected = false;

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



    public void MenuCursorUpDown() {

        RowSelected = !RowSelected;

    }

    public void MenuCursorLeftRight() {

        MenuSelected = !MenuSelected;

    }

    public void MenuCursorEnter() {

        // god im bad at my job this solution sucks

        if (RowSelected == false) {

            // on top
            if (MenuSelected == false) {

                // top left

                Debug.Log("11");

                VersusSelected();

            }

            if (MenuSelected == true) {

                // top right

                Debug.Log("12");

                StorySelected();

            }
        } else {

            // lower row 

                if (MenuSelected == false) {

                // btm left

                Debug.Log("21");

                SettingsSelected();

            }

            if (MenuSelected == true) {

                // btm right

                Debug.Log("22");

                ReturnToTitle();

            }
        }

    }
    
        

}
