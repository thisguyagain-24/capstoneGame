using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEditor.UI;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using JetBrains.Annotations;
using UnityEngine.InputSystem.iOS;

public class CharSelect : MonoBehaviour
{

    [SerializeField]
    public GameObject[] CharacterButtons;

    public int[,] UsableButtonsArray = {{1,2},{3,4}};

    public int rowSelected = 0;

    public int colSelected = 0;

    public int selectedButton = 0;

    public Player player;

    public void Start() {

        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            p.GetComponent<Player>().FindUIDirector();
        }

        UsableButtonsArray[0, 0] = 1;

        UsableButtonsArray[0, 1] = 2;

        UsableButtonsArray[1, 0] = 3;

        UsableButtonsArray[1, 1] = 4;

        //Debug.Log(player);

        //Debug.Log(UsableButtonsArray);

    }

    public void UpdateSelected(int direction)
    {

        switch (direction)
        {

            case 4:

                colSelected = Math.Abs((colSelected - 1) % UsableButtonsArray.GetLength(0));

                Debug.Log(colSelected.ToString() + " " + rowSelected.ToString());

                break;

            case 6:

                colSelected = Math.Abs((colSelected + 1) % UsableButtonsArray.GetLength(0));

                Debug.Log(colSelected.ToString() + " " + rowSelected.ToString());


                break;

            case 2:

                rowSelected = Math.Abs((rowSelected + 1) % UsableButtonsArray.GetLength(0));

                Debug.Log(colSelected.ToString() + " " + rowSelected.ToString());

                break;

            case 8:

                rowSelected = Math.Abs((rowSelected - 1) % UsableButtonsArray.GetLength(0));

                Debug.Log(colSelected.ToString() + " " + rowSelected.ToString());

                break;
        }

        if (colSelected == 0 && rowSelected == 0){
            selectedButton = 0;
        } else if (colSelected == 1 && rowSelected == 0) {
            selectedButton = 1;
        } else if (colSelected == 0 && rowSelected == 1) {
            selectedButton = 2;
        } else {
            selectedButton = 3;
        }
    }

    public void MenuCursorEnter() {
        if(player)
        player.MakeCharacter(0);


    }

}