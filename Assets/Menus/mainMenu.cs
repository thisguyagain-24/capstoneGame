using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public GameObject SettingsMenu;

    [SerializeField]
    public GameObject[] menubuttons;

<<<<<<< HEAD
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
=======
    void Start() {
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            p.GetComponent<Player>().FindUIDirector();
        }
    }
        

>>>>>>> e37933b1d4219a90d2dc30737c6df3b8717bb350
}
