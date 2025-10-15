using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using JetBrains.Annotations;
using UnityEditor.UI;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject SettingsMenu;


    [SerializeField]
    public GameObject[] menuButtons;

    [SerializeField]
    public GameObject[] row1;

    [SerializeField]
    public GameObject[] row2;

    [SerializeField]
    public Color highlightColor = new(1, 1, 1, 0.5f);

    public Color baseColor = new(1, 1, 1, 1);

    public bool RowSelected = false;
    public bool MenuSelected = false;

    public int currentSelected = 0;

    public SpriteRenderer[]menuButtonsConverted = new SpriteRenderer[4];

    void Start() {
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            p.GetComponent<Player>().FindUIDirector();
        }

        for (int i = 0; i < menuButtons.Length; i++) {

            menuButtonsConverted[i] = menuButtons[i].GetComponent<SpriteRenderer>();

            Debug.Log(menuButtonsConverted[i]);
        }

        PickHighlight();

    }

    void VersusSelected() 
    {

        Debug.Log("versus selected");

        // play sound effect, play animation, then load versus scene

        // hmmmmm sfx and animation can probably be in their own reusable function once im putting those in

    }

    void StorySelected() 
    {

        Debug.Log("story selected");

        // popup that tells you its not ready yet

    }

    void SettingsSelected() 
    {

        Debug.Log("settings selected");

        // load settings menu

    }

    void ReturnToTitle() {

        // play sound effect, play animation, then load title menu

        SceneManager.LoadScene("startMenu");

    }


    public void MenuCursorUpDown() {

        RowSelected = !RowSelected;

        PickHighlight();

    }

    public void MenuCursorLeftRight() {

        MenuSelected = !MenuSelected;

        PickHighlight();

    }

    public void MenuCursorEnter() {

        // god im bad at my job this solution sucks

        PickHighlight();

        switch (currentSelected) {

            case 0:

                VersusSelected();

                break;

            case 1:

                StorySelected();

                break;

            case 2:

                SettingsSelected();

                break;

            case 3:

                ReturnToTitle();

                break;

        }

    }
    
    public void PickHighlight() {

        menuButtonsConverted[currentSelected].color = baseColor;

        menuButtonsConverted[currentSelected].transform.localScale = new Vector3(13,13,0);

        if (RowSelected == false) {

            // on top
            if (MenuSelected == false) {

                // top left

                currentSelected = 0;
                

            }

            if (MenuSelected == true) {

                // top right

                currentSelected = 1;

            }

        } 
        else if (RowSelected == true){

            // lower row 

            if (MenuSelected == false) {

                // btm left

                currentSelected = 2;

            }

            if (MenuSelected == true) {

                // btm right

                currentSelected = 3;

            }
            
        }

        menuButtonsConverted[currentSelected].color = highlightColor;

        menuButtonsConverted[currentSelected].transform.localScale = new Vector3(10,10,0);

    }

}
