using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using Unity.Burst;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FightSceneManager : MonoBehaviour
{
    public float animationTimeRemaining;
    public bool inRoundStart;
    public bool inRoundEnd;
    public bool inGameOver;

    public GameObject[] healthBarSprite = new GameObject[2];
    public GameObject[] healthBarOuter = new GameObject[2];
    public GameObject[] roundsTxt;
    public GameObject[] roundsTicks;
    public GameObject[] GameEndDialog;
    public GameObject StaticGameEndDialog;

    public Player[] players = new Player[2];
    public Fighter[] fighters = new Fighter[2];

    public int selectedButton = 0;
    public GameObject[] pauseButtons;
    public GameObject pauseMenu;

    public readonly int rounds = 2;

    public int roundsElapsed = 0;

    public Vector3 healthBarScale;

    public bool pause = false;
    public Vector3 baseSize;

    public bool canTheyDoStuff; // yayyy i get one silly variable name

    // Start is called before the first frame update
    void Start(){

        foreach (GameObject f in GameObject.FindGameObjectsWithTag("fighter"))
        {
            Fighter _f = f.GetComponent<Fighter>();
            fighters[_f.playerNum] = _f;
            Debug.Log("got fighter " + _f);
            _f.FindFightSceneManager();

        }

        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            Player _p = p.GetComponent<Player>();
            _p.FindUIDirector();
            players[_p.playerNum] = _p;
        }

        baseSize = pauseButtons[0].transform.localScale;

        healthBarScale = healthBarSprite[0].transform.localScale;
    }

#region GameplayUI
    public double CalcGuiHealth(int _fighter) {
        return fighters[_fighter].currHealth - (fighters[_fighter].maxHealth / 20);
    }

    public void UpdateGUIHealth(int playerNum) {

        Debug.Log("hello GUIhealth");

        Transform barEnd = healthBarOuter[playerNum].transform;
        Transform barMid = healthBarSprite[playerNum].transform;

        double healthPercent = CalcGuiHealth(playerNum) / fighters[playerNum].maxHealth;

        Debug.Log(playerNum + "GUIhealth " + CalcGuiHealth(playerNum));
        Debug.Log(playerNum + "realhealth " + fighters[playerNum].currHealth);

        if (healthPercent<=0){
            barMid.localScale = new Vector3((float)healthPercent * (float)0,healthBarScale.y,healthBarScale.z);
        } else {
            barMid.localScale = new Vector3((float)healthPercent * (float)healthBarScale.x,healthBarScale.y,healthBarScale.z);
        }

        barEnd.localPosition = new Vector3(barMid.localPosition.x + barMid.GetComponent<SpriteRenderer>().bounds.size.x, barEnd.localPosition.y, barEnd.localPosition.z);

    }

    public void GuiHealthReset() {

        for (int i = 0; i < healthBarSprite.Length; i++ )
        {
            GameObject bar = healthBarSprite[i];
            GameObject outerbar = healthBarOuter[i];

            Transform barMid = bar.transform;
            barMid.localScale = new Vector3(healthBarScale.x,healthBarScale.y,healthBarScale.z);

            outerbar.transform.localPosition = new Vector3(barMid.localPosition.x + barMid.GetComponent<SpriteRenderer>().bounds.size.x, outerbar.transform.localPosition.y, outerbar.transform.localPosition.z);
        }
    }

    public void GuiHealthDie(int playerNum) {
        healthBarSprite[playerNum].SetActive(false);
        healthBarOuter[playerNum].SetActive(false);
    }
#endregion

#region Gameplay

    public void PlayerDamageUpdate(int playerNum) {
        UpdateGUIHealth(playerNum);
    }

    public void RoundEnd(int loserNum) {

        roundsElapsed ++;

        canTheyDoStuff = false;

        Fighter loser = fighters[loserNum];
        loser.lives --;

        fighters[loserNum].EnableLoss(120f);
        fighters[(loserNum+1)%2].EnableVictory(120f);

        animationTimeRemaining = 120;
        inRoundEnd = true;

        Debug.Log("Round Ended");

        foreach (var fighter in fighters)
        {
            fighter.DisableActiveMove();
            fighter.movementSprites.SetActive(true);
            if (fighter.lives == 0) {
                inGameOver = true;
                GameOver(fighter.playerNum);
            }
        }

    }

    public void RoundStart() {
        fighters[0].transform.localPosition = new Vector3(-300, -310, 1);
        fighters[1].transform.localPosition = new Vector3(300, -310, 1);

        foreach (var fighter in fighters)
        {
            Debug.Log(fighter.maxHealth);
            fighter.currHealth = fighter.maxHealth;
            fighter.EnableRoundStart(140);

            healthBarSprite[fighter.playerNum].SetActive(true);
            healthBarOuter[fighter.playerNum].SetActive(true);
        }

        GuiHealthReset();

        roundsTxt[roundsElapsed].SetActive(true);

        animationTimeRemaining = 120;
        inRoundStart = true;

        Debug.Log("Round Started");

    }

    public void GameOver(int _loserNum) {
        Debug.Log("Game Ended with" + fighters[_loserNum] + "loss");

        GameEndDialog[(_loserNum+1)%2].SetActive(true);
        StaticGameEndDialog.SetActive(true);

    }

    public void HandleReset() {
        foreach (var fighter in fighters)
        {
            fighter.lives = fighter.maxLives;
            fighter.currHealth = fighter.maxHealth;
            fighter.burst = fighter.maxBurst;
            RoundStart(); 
        }
    }

#endregion

#region Menu

    public void PauseMenuHandler() {
        pause = !pause;
        if(pause){
            Debug.Log("PAUSE");
            Time.timeScale = 0;
            players[0].pInput.SwitchCurrentActionMap("UI");
            PauseUpdateGui();
            pauseMenu.SetActive(true);
        }else if (pause && !inGameOver){
            Debug.Log("UNPAUSE");
            Time.timeScale = 1;
            players[0].pInput.SwitchCurrentActionMap("Player");
            pauseMenu.SetActive(false);
        } else if (pause && inGameOver){
            Debug.Log("FORCE EXIT");
        }
    }

    public void PauseUpdateSelected(int direction){
        Debug.Log("direction " + direction);
        switch (direction)
        {
            case 2:
                selectedButton = Math.Abs((selectedButton - 1) % pauseButtons.Length);
                break;
            case 8:
                selectedButton = Math.Abs((selectedButton + 1) % pauseButtons.Length);
                break;
        }
        PauseUpdateGui();
    }

    public void PauseCursorSelected(){
        Debug.Log("selected button " + selectedButton);
        if(selectedButton == 0){
            PauseMenuHandler();
        } else if (selectedButton == 0 && inGameOver){
            HandleReset();
        } else if (selectedButton == 1) {
            Time.timeScale = 1;
            SceneManager.LoadScene("mainMenu",LoadSceneMode.Single);
        }
    }

    public void PauseUpdateGui(){
        foreach (var item in pauseButtons)
        {
            item.transform.localScale = new Vector3(14f,14f,0);
        }
        pauseButtons[selectedButton].transform.localScale = baseSize;
    }

    public void InvokeEndMenu(){ //hi
        PauseMenuHandler();
    }

#endregion
   

    // Update is called once per frame

    void FixedUpdate()
    {
        if(animationTimeRemaining >= 0){
            animationTimeRemaining --;

        } else if(animationTimeRemaining <= 0 && inRoundStart) {

            roundsTxt[roundsElapsed].SetActive(false);
            inRoundStart = false;

            Debug.Log("RoundStart End");

        } else if (animationTimeRemaining <= 0 && inRoundEnd) {
            inRoundEnd = false;
            RoundStart();
            Debug.Log("RoundEnd End");

        } else if (animationTimeRemaining <= 0 && inGameOver){
        }
    }
}
