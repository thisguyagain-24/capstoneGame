using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class CharSelect : MonoBehaviour
{

    [SerializeField]
    public GameObject[] CharacterButtons;
    public GameObject[,] UsableButtonsArray = new GameObject[2,2];

    public int rowSelected = 0;
    public int colSelected = 0;
    public int selectedButton = 0;

    public Vector3 defaultSize;
    public Color defaultClr;

    public Player[] players = new Player[2];

    public Color baseColor = new(1, 1, 1, 1);

    public void Start() {

        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            Player _p = p.GetComponent<Player>();
            _p.FindUIDirector();
            players[_p.playerNum] = _p;
        }

        UsableButtonsArray[0, 0] = CharacterButtons[0];
        UsableButtonsArray[0, 1] = CharacterButtons[1];
        UsableButtonsArray[1, 0] = CharacterButtons[2];
        UsableButtonsArray[1, 1] = CharacterButtons[3];

        defaultClr = Color.white;

        //Debug.Log(player);
        //Debug.Log(UsableButtonsArray);
    }

    public void UpdateSelected(int direction, int playerNum)
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

        if (playerNum == 0) {

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


        GuiUpdateSelected();
    }

    public void MenuCursorEnter(int playerNum) {        
        players[playerNum].MakeCharacter(0);

        if (players[0].fighter && players[1].fighter) {
            players[0].fighter.transform.localPosition = new Vector3(-150, -310, 1);
            players[0].fighter.transform.localScale = new Vector3(600, 600, 0);
            
            players[1].fighter.transform.localPosition = new Vector3(150, -310, 1);
            players[1].fighter.transform.localScale = new Vector3(600, 600, 0);
            
            players[0].pInput.SwitchCurrentActionMap("Player");
            players[1].pInput.SwitchCurrentActionMap("Player");

            SceneManager.LoadScene("fightScene");
        }
    }

    public void GuiUpdateSelected() {
        ClearHighlight();
        PickHighlight();
    }

    public void PickHighlight() {

        Debug.Log("picking highlight");

        GameObject o = UsableButtonsArray[rowSelected, colSelected];

        foreach (TextMeshPro text in o.GetComponentsInChildren<TextMeshPro>())
        {
            text.color = new Color(0, 0, 1, 1);
            text.transform.localScale = new Vector3(1.5f,1.5f,0);
        }
        foreach (Image image in o.GetComponentsInChildren<Image>())
        {
            image.color = new Color(0, 0, 0.7f, 1);
            if (image.tag == "icon") {
                image.transform.localScale = new Vector3(1.9f,1.9f,0);
            } else {
                image.transform.localScale = new Vector3(2f,2f,0);
            }
            
        }
    }

    public void ClearHighlight() {
        
        Debug.Log("unhighlight");
        
        foreach (var button in UsableButtonsArray)
        {
            foreach (TextMeshPro text in button.GetComponentsInChildren<TextMeshPro>())
                {
                    text.color = defaultClr;
                }
                foreach (Image image in button.GetComponentsInChildren<Image>())
                {
                    image.color = defaultClr;
                    if (image.tag == "icon") {
                        image.transform.localScale = new Vector3(2.4f,2.4f,0);
                    } else {
                        image.transform.localScale = new Vector3(2.5f,2.5f,0);
                    }
                }

        }


    }
}