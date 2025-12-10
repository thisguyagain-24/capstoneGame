using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Timers;
using Unity.Burst;
using UnityEditor.Build.Player;
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

    public int selectedButton = 0;
    public GameObject[] pauseButtons;
    public GameObject pauseMenu;

    public readonly int rounds = 2;

    public int roundsElapsed = 0;

    public Vector3 healthBarScale;

    public bool paused = false;
    public Vector3 baseSize;

    public bool canTheyDoStuff; // yayyy i get one silly variable name

    public SpriteRenderer[] faceZones;
    public int faceResetTimer = 0;

    public GameObject kaboom;
    public GameObject activeKaboom;

    // Start is called before the first frame update
    void Start(){

        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            Player _p = p.GetComponent<Player>();
            //Debug.Log("FOUND PLAYER " + _p.playerNum);
            _p.FindUIDirector();
            players[_p.playerNum] = _p;
            _p.fighter.FindFightSceneManager();
            GuiFaceReset(_p.playerNum);
        }

        baseSize = pauseButtons[0].transform.localScale;

        healthBarScale = healthBarSprite[0].transform.localScale;

    }

#region GameplayUI
    public double CalcGuiHealth(int _fighter) {
        return players[_fighter].fighter.currHealth - (players[_fighter].fighter.maxHealth / 20);
    }

    public void UpdateGUIHealth(int playerNum) {

        //debug.Log("hello GUIhealth");

        Transform barEnd = healthBarOuter[playerNum].transform;
        Transform barMid = healthBarSprite[playerNum].transform;

        double healthPercent = CalcGuiHealth(playerNum) / players[playerNum].fighter.maxHealth;

        //Debug.Log(playerNum + "GUIhealth " + CalcGuiHealth(playerNum));
        //Debug.Log(playerNum + "realhealth " + players[playerNum].fighter.currHealth);

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

    public void GuiFaceChanger(int playerNum, int face) {

        faceResetTimer = 100;

        faceZones[playerNum].GetComponent<SpriteRenderer>().sprite = players[playerNum].fighter.fighterFaces[face];
        //Debug.Log(playerNum);

    }

    public void GuiFaceReset(int playerNum) {

        faceResetTimer = 100;

        //Debug.Log(playerNum);
        faceZones[playerNum].GetComponent<SpriteRenderer>().sprite = players[playerNum].fighter.fighterFaces[3];
    
    }


#endregion

#region Gameplay

    public void PlayerDamageUpdate(int playerNum) {
        UpdateGUIHealth(playerNum);
        GuiFaceChanger(playerNum,2);
    }

    public void RoundEnd(int loserNum) {

        roundsElapsed ++;

        canTheyDoStuff = false;

        Fighter loser = players[loserNum].fighter;
        loser.lives --;

        players[loserNum].fighter.EnableLoss(120f);
        players[(loserNum+1)%2].fighter.EnableVictory(120f);

        animationTimeRemaining = 120;
        inRoundEnd = true;

        //Debug.Log("Round Ended");
        foreach(Player p in players){
            p.fighter.DisableActiveMove();
            p.fighter.movementSprites.SetActive(true);
            if (p.fighter.lives == 0) {
                players[0].pInput.SwitchCurrentActionMap("UI");
                inGameOver = true;
                GameOver(p.fighter.playerNum);
            }
        }

        activeKaboom = Instantiate(kaboom, new Vector3 (0,0,0), new Quaternion(0,0,0,0));
        activeKaboom.transform.localScale = new Vector3(400, 400, 0);
        activeKaboom.transform.localPosition = loser.transform.localPosition;

        loser.transform.localScale = new Vector3(0,0,0);

    }

    public void RoundStart() {

        foreach (Player p in players) {
            p.fighter.transform.localScale = new Vector3(800,800,1);
        }

        if (activeKaboom) {
            Destroy(activeKaboom);
        }
        
        players[0].fighter.transform.localPosition = new Vector3(-300, -310, 1);
        players[1].fighter.transform.localPosition = new Vector3(300, -310, 1);

        foreach (Player p in players)
        {
            //Debug.Log(p.fighter.maxHealth);
            p.fighter.currHealth = p.fighter.maxHealth;
            p.fighter.EnableRoundStart(140);

            healthBarSprite[p.fighter.playerNum].SetActive(true);
            healthBarOuter[p.fighter.playerNum].SetActive(true);

            GuiFaceReset(p.playerNum);
        }

        GuiHealthReset();

        roundsTxt[roundsElapsed].SetActive(true);   
        
        animationTimeRemaining = 120;
        inRoundStart = true;

        //Debug.Log("Round Started");

    }

    public void GameOver(int _loserNum) {
        //Debug.Log("Game Ended with" + players[_loserNum].fighter + "loss");

        GameEndDialog[(_loserNum+1)%2].SetActive(true);
        StaticGameEndDialog.SetActive(true);

    }

    public void HandleReset() {
        roundsElapsed = 0;
        foreach (Player p in players)
        {
            inRoundEnd = false;
            p.fighter.lives = p.fighter.maxLives;
            p.fighter.currHealth = p.fighter.maxHealth;
            UnPauseGame();
            RoundStart(); 
        }
    }

#endregion

#region Menu

    public void PauseMenuHandler() {
        
        if (paused && !inGameOver){

        } else if (paused){

        }

    }

    public void PauseGame() {
        paused = true;
        //Debug.Log("PAUSE");
        Time.timeScale = 0;
        players[0].pInput.SwitchCurrentActionMap("UI");
        PauseUpdateGui();
        pauseMenu.SetActive(true);
    }

    public void UnPauseGame() {
        paused = false;
        //Debug.Log("UNPAUSE");
        Time.timeScale = 1;
        players[0].pInput.SwitchCurrentActionMap("Player");
        pauseMenu.SetActive(false);
    }

    public void PauseUpdateSelected(int direction){
        //Debug.Log("direction " + direction);
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
        //Debug.Log("selected button " + selectedButton);
        if(selectedButton == 0){
            if(inGameOver){
                HandleReset();
            }else{
                UnPauseGame();
            }
        }else if(selectedButton == 1){
            Time.timeScale = 1;
            foreach(Player p in players){
                Destroy(p.fighter.gameObject);
            
                p.pInput.SwitchCurrentActionMap("UI");
            }
            SceneManager.LoadScene("mainMenu",LoadSceneMode.Single);
        }
        
        /*
        } else if (selectedButton == 0 && inGameOver){
            HandleReset();
        } else if (selectedButton == 1) {
            Time.timeScale = 1;
            SceneManager.LoadScene("mainMenu",LoadSceneMode.Single);
        }*/
    }

    public void PauseUpdateGui(){
        foreach (var item in pauseButtons)
        {
            item.transform.localScale = new Vector3(14f,14f,0);
        }
        pauseButtons[selectedButton].transform.localScale = baseSize;
    }

    public void PlayerUISubmit() {
        if(paused){
            PauseCursorSelected();
        }else{
            if(inGameOver){
                InvokeEndMenu();
            }
        }
    }

    public void InvokeEndMenu() { //hi
        //pause = true;


        GameEndDialog[0].SetActive(false);
        GameEndDialog[1].SetActive(false);
        StaticGameEndDialog.SetActive(false);

        PauseGame();
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

            //Debug.Log("RoundStart End");

        } else if (animationTimeRemaining <= 0 && inRoundEnd) {
            inRoundEnd = false;
            RoundStart();
            //Debug.Log("RoundEnd End");

        } else if (animationTimeRemaining <= 0 && inGameOver){

        }

        if(faceResetTimer <= 0) {
            GuiFaceReset(0);
            GuiFaceReset(1);
        }

        foreach (Player p in players)
        {
            if (p.fighter.activeMove) {
                GuiFaceChanger(p.playerNum, 1);
            }
        }

        faceResetTimer --;
    }
}
