using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using System.Linq;
using System;
using Unity.Mathematics;

public class Fighter : MonoBehaviour
{
    public double maxHealth;
    public double currHealth;
    
    public double incomingDamageModifier = 1;

    public int playerNum;

    public int maxLives;
    public int lives;

    public int maxTension;
    public int tension;

    public int maxBurst;
    public int burst;

    public float forwardWalkSpeed;
    public float backWalkSpeed;

    public Animator animator;

    public int inputDirection;
    
    [HideInInspector]
    public bool inAir;

    [HideInInspector]
    public bool crouching;

    [HideInInspector]
    public bool neutral;

    [HideInInspector]
    public bool startup;

    [HideInInspector]
    public bool active;

    [HideInInspector]
    public bool recovery;

    [HideInInspector]
    public bool softKnockdown;

    [HideInInspector]
    public bool hardKnockdown;

    [HideInInspector]
    public bool hitstun;

    [HideInInspector]
    public bool blockstun;

    public bool blocking;

    public bool leftSide;

    public GameObject movementSprites;
    public Rigidbody2D rb;

    public FighterMove[] moves;
    public FighterMove activeMove;
    
    public GameObject defaultHurtbox;
    public GameObject hitStunObj;

    public bool inHitstop;
    public float hitstopDuration;
    public float hitstopFramesElapsed;
    
    public Fighter opponent;

    public FightSceneManager fightSceneManager;

    public void Die()
    {

        fightSceneManager.GuiHealthDie(playerNum);

    }

    // Start is called before the first frame update
    void Start()
    {
        inputDirection = 5;
        defaultHurtbox.layer = getPlayerHurtboxLayer();
        hitStunObj.transform.GetChild(1).gameObject.layer = getPlayerHurtboxLayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (inHitstop)
        {
            IterateHitstop();
        }
        if (!opponent)
        {
            leftSide = true;
            foreach(Fighter f in GameObject.FindObjectsByType<Fighter>(FindObjectsSortMode.None))
            {
                if(f != this)
                {
                    opponent = f;
                }
            }
        }
        else
        {
            CheckSide();
        }
        if (inputDirection == 0)
        {
            Debug.LogError("P" + playerNum + " HAS INVALID INPUT DIRECTION");
        }
        if (inputDirection == 5)
        {
            Neutral();   
        }
        else
        {
            //Debug.Log("KNIGHT MOVING WITH " + inputDirection);
        }
        if (inputDirection.In(1, 2, 3))
        {
            Crouch();
        }
        else if (inputDirection.In(6, 9))
        {
            WalkForward();
        }
        else if (inputDirection.In(4, 7))
        {
            WalkBack();
        }

        if(inputDirection.In(1,4,7)){

        }
    }

    public void Neutral()
    {
        animator.SetBool("Crouch", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Back", false);
        animator.SetBool("Neutral", true);
    }

    public void Crouch()
    {
        animator.SetBool("Crouch", true);
        animator.SetBool("Walk", false);
        animator.SetBool("Back", false);
        animator.SetBool("Neutral", false);
    }

    public void WalkForward()
    {
        //Vector2 moveDir = leftSide ? Vector2.right : Vector2.left;
        Vector2 moveDir = Vector2.right;
        
        Debug.Log("Before scale " + moveDir);
        moveDir *= forwardWalkSpeed * (float)Math.Min(0.05f, Time.deltaTime * 60) * transform.lossyScale.x;
        Debug.Log("After scale " + moveDir);
        rb.MovePosition(rb.position + moveDir);

        animator.SetBool("Crouch", false);
        animator.SetBool("Walk", true);
        animator.SetBool("Back", false);
        animator.SetBool("Neutral", false);
    }
    
    public void WalkBack()
    {
        //Vector2 moveDir = leftSide ? Vector2.left : Vector2.right;
        Vector2 moveDir = Vector2.left;
        Debug.Log("Before scale " + moveDir);
        moveDir *= backWalkSpeed * (float)Math.Min(0.05f, Time.deltaTime * 60) * transform.lossyScale.x;
        Debug.Log("After scale " + moveDir);
        rb.MovePosition(rb.position + moveDir);
        
        animator.SetBool("Crouch", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Back", true);
        animator.SetBool("Neutral", false);
    }

    public void EnableHitstun(float stopDur, float stunDir)
    {
        if (activeMove)
        {
            foreach (MoveFrame o in activeMove.keys)
            {
                o.gameObject.SetActive(false);
            }

            activeMove.active = false;
            activeMove = null;
        }
        movementSprites.SetActive(false);
        hitStunObj.SetActive(true);
        EnableHitstop(stopDur + stunDir);
    }

    public void EnableHitstop(float _dur)
    {
        inHitstop = true;
        hitstopFramesElapsed = 0;
        hitstopDuration = _dur;
    }

    public void IterateHitstop()
    {
        hitstopFramesElapsed += Time.deltaTime * 60;
        //Debug.Log(hitstopFramesElapsed + " / " + hitstopDuration);
        if (hitstopDuration <= hitstopFramesElapsed)
        {
            inHitstop = false;
            hitStunObj.SetActive(false);
            EndForcedAnim();
        }
    }

    public void SubHealth(double damage)
    {
        currHealth = currHealth - (damage * incomingDamageModifier);

        if(currHealth <= 0){
            currHealth = 0;
            Die();
        }

        fightSceneManager?.PlayerDamageUpdate(playerNum);

    }

    public void OnMove(int dir)
    {
        Debug.Log("SHMOVIN");
        inputDirection = dir;
    }

    public void GetMoves()
    {
        moves = this.GetComponentsInChildren<FighterMove>();
    }

    public void OnLight()
    {
        if (!inHitstop && !(activeMove ? activeMove.active : false)){
            Debug.Log("P" + playerNum + " Light");
            foreach (FighterMove fm in moves)
            {
                Debug.Log(fm.btn);
                if (fm.btn == FighterMove.AttackButton.L)
                {
                    Debug.Log("Found a L move");
                    if (fm.inputDirection.Contains(inputDirection))
                    {
                        Debug.Log("Found matching move");
                        movementSprites.SetActive(false);
                        activeMove = fm;
                        fm.StartMove();
                        return;
                    }
                }
            }
        }
    }

    public void DoneMove()
    {
        activeMove = null;
        EndForcedAnim();
    }

    public void EndForcedAnim()
    {
        if (!activeMove)
        {
            movementSprites.SetActive(true);
        }
    }

    public void OnHeavy()
    {
        if (!inHitstop && !(activeMove ? activeMove.active : false)){
            Debug.Log("P" + playerNum + " Heavy");
            foreach (FighterMove fm in moves)
            {
                Debug.Log(fm.btn);
                if (fm.btn == FighterMove.AttackButton.H)
                {
                    Debug.Log("Found an H move");
                    if (fm.inputDirection.Contains(inputDirection))
                    {
                        Debug.Log("Found matching move");
                        movementSprites.SetActive(false);
                        activeMove = fm;
                        fm.StartMove();
                        return;
                    }
                }
            }
        }
    }

    public void OnUniversal()
    {
        
    }

    public void OnSpecial()
    {

    }

    public void CheckSide()
    {
        if (opponent)
        {
            if (opponent.transform.position.x > transform.position.x)
            {
                //Debug.Log("LEFT SIDE");
                Vector3 flipped = transform.localScale;
                flipped.x = math.abs(flipped.x);
                transform.localScale = flipped;
                leftSide = true;
            }
            else
            {
                //Debug.Log("RIGHT SIDE");
                Vector3 flipped = transform.localScale;
                flipped.x = math.abs(flipped.x) * -1;
                transform.localScale = flipped;
                leftSide = false;
            }
        }
    }

    public int getPlayerHurtboxLayer()
    {
        if (playerNum == 0)
        {
            return LayerMask.NameToLayer("Player1Hurtbox");
        }
        else
        {
            return LayerMask.NameToLayer("Player2Hurtbox");
        }
    }

    // hi im sorry for infesting your lovely code i need a function here

    public void FindFightSceneManager(){

        fightSceneManager = GameObject.Find("fightSceneManager").GetComponent<FightSceneManager>();
        Debug.Log("found fightSceneManager " + fightSceneManager);

    }
    
    
}






