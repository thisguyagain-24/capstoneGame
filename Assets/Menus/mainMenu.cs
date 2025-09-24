using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public GameObject SettingsMenu;

    [SerializeField]
    public GameObject[] menubuttons;

    void Start() {
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            p.GetComponent<Player>().FindUIDirector();
        }
    }
        

}
