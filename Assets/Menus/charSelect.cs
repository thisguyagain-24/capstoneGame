using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEditor.UI;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
using Unity.VisualScripting;

public class CharSelect : MonoBehaviour
{

    [SerializeField]
    public GameObject[] CharacterButtons;


    public int[,] UsableButtonsArray = {{1,2},{3,4}};

    public int rowSelected = 0;

    public int colSelected = 0;

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

        Debug.Log(player);

        //Debug.Log(UsableButtonsArray);

    }

    public void UpdateSelected(int direction)
    {

        switch (direction)
        {

            case 4:

                colSelected = Math.Abs((colSelected-1) % UsableButtonsArray.GetLength(0));

                Debug.Log(colSelected.ToString() + " " + rowSelected.ToString());

                break;

            case 6:

            
                colSelected = Math.Abs((colSelected+1) % UsableButtonsArray.GetLength(0));

                Debug.Log(colSelected.ToString() + " " + rowSelected.ToString());


                break;

            case 2:

                rowSelected = Math.Abs((rowSelected+1) % UsableButtonsArray.GetLength(0));

                Debug.Log(colSelected.ToString() + " " + rowSelected.ToString());

                break;

            case 8:

                rowSelected = Math.Abs((rowSelected-1) % UsableButtonsArray.GetLength(0));

                Debug.Log(colSelected.ToString() + " " + rowSelected.ToString());

                break;
        }

    }

}