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
    public int[] selectedButton;
    public int[] prevselected = {0,0};

    public Vector3 defaultSize;
    public Color defaultClr;

    public GameObject[] p1Icons;
    public GameObject[] p2Icons;

    public Player[] players = new Player[2];

    public Color baseColor = new(1, 1, 1, 1);

    public void Start() {

        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            Player _p = p.GetComponent<Player>();
            _p.FindUIDirector();
            players[_p.playerNum] = _p;
            GuiUpdateSelected(_p.playerNum);
        }
        selectedButton = new int[]{0,0};
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
                break;
            case 6:
                colSelected = Math.Abs((colSelected + 1) % UsableButtonsArray.GetLength(0));
                break;
            case 2:
                rowSelected = Math.Abs((rowSelected + 1) % UsableButtonsArray.GetLength(0));
                break;
            case 8:
                rowSelected = Math.Abs((rowSelected - 1) % UsableButtonsArray.GetLength(0));
                break;
        }
        /*/
        Debug.Log(colSelected.ToString() + " " + rowSelected.ToString());
        Debug.Log("Hello " + playerNum);
        Debug.Log(selectedButton.Length);
        //*/
        if (colSelected == 0 && rowSelected == 0){
            selectedButton[playerNum] = 0;
        } else if (colSelected == 1 && rowSelected == 0) {
            selectedButton[playerNum] = 1;
        } else if (colSelected == 0 && rowSelected == 1) {
            selectedButton[playerNum] = 2;
        } else {
            selectedButton[playerNum] = 3;
        }

        GuiUpdateSelected(playerNum);
    }

    public void MenuCursorEnter(int playerNum) {        
        players[playerNum].MakeFighter();

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

    public void GuiUpdateSelected(int playerNum) {
        //ClearHighlight();
        //PickHighlight();

        prevselected[playerNum] = selectedButton[playerNum];

        ClearPlayerIcon(playerNum);
        PickPlayerIcon(playerNum);
    }

    public void PickHighlight() {

        //Debug.Log("picking highlight");

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

    public void PickPlayerIcon(int playerNum) {
        if (playerNum == 0) {
            p1Icons[selectedButton[playerNum]].SetActive(true);
        } else {
            p2Icons[selectedButton[playerNum]].SetActive(true);
        }
    }

    public void ClearPlayerIcon(int playerNum) {
        if (playerNum == 0) {
            foreach (var item in p1Icons)
            {
                item.SetActive(false);
            }
        } else {
            foreach (var item in p2Icons)
            {
                item.SetActive(false);
            }
        }
    }




    public void ClearHighlight() {
        
        //Debug.Log("unhighlight");
        
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