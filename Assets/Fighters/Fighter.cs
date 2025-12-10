using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using System.Linq;
using System;
using Unity.Mathematics;
using Unity.VisualScripting;

public class Fighter : MonoBehaviour
{
    public String fighterName;

    public Sprite[] fighterFaces;
    
    public double maxHealth;
    
    [HideInInspector]
    public double currHealth;

    public double incomingDamageModifier = 1;
    
    [HideInInspector]
    public int playerNum;

    public int maxLives;
    public int lives;

    public float forwardWalkSpeed;
    public float backWalkSpeed;

    public Animator animator;

    [HideInInspector]
    public int inputDirection;

    [HideInInspector]
    public bool crouching;

    [HideInInspector]
    public bool neutral;

    [HideInInspector]
    public bool startup;

    [HideInInspector]
    public bool hitstun;

    [HideInInspector]
    public bool blockstun;

    [HideInInspector]
    public bool hiBlocking;
    [HideInInspector]
    public bool lowBlocking;

    [HideInInspector]
    public bool leftSide;

    public GameObject movementSprites;
    public Rigidbody2D rb;

    public FighterMove[] moves;
    public FighterMove activeMove;
    
    public GameObject defaultHurtbox;
    public GameObject hitStunObj;
    public GameObject blockStunObj;

    public bool canTheyDoStuff;
    [HideInInspector]
    public float forcedAnimDuration;
    [HideInInspector]
    public float forcedAnimElapsed;
    
    [HideInInspector]
    public Fighter opponent;

    public FightSceneManager fightSceneManager;

    [HideInInspector]
    public float knockbackStrength;

    public AudioClip[] ouchAudioClips;
    public AudioSource audioSource;
    public AudioClip walkies;
    public AudioClip[] victoryClips;
    public AudioClip[] randomIntros;
    public AudioClip labelMakerQuestion;
    public AudioClip labelMakerAnswer;

    public void Die()
    {
        fightSceneManager?.GuiHealthDie(playerNum);
        fightSceneManager?.RoundEnd(playerNum);
    }

    // Start is called before the first frame update
    void Start()
    {
        inputDirection = 5;
        defaultHurtbox.layer = getPlayerHurtboxLayer();
        hitStunObj.transform.GetChild(1).gameObject.layer = getPlayerHurtboxLayer();
        blockStunObj.transform.GetChild(1).gameObject.layer = getPlayerHurtboxLayer();
        hitStunObj.SetActive(false);
        blockStunObj.SetActive(false);
    }

    public void FindFightSceneManager()
    {
        fightSceneManager = GameObject.Find("fightSceneManager").GetComponent<FightSceneManager>();
        Debug.Log("found fightSceneManager " + fightSceneManager);
    }

    public void OnMovementInput(int dir)
    {
        //Debug.Log("SHMOVIN");
        inputDirection = dir;
    }

    private void findOpponent()
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
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!opponent)
        {
            findOpponent();
        }
        CheckSide();
        if (canTheyDoStuff)
        {
            IterateForcedAnim();
        }
        else
        {
            if (inputDirection == 0)
            {
                Debug.LogError("P" + playerNum + " HAS INVALID INPUT DIRECTION");
            }
            else if (inputDirection == 5)
            {
                Neutral();   
            }
            else if (inputDirection.In(1, 2, 3))
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
            
            if(inputDirection == 1){
                lowBlocking = true;
            }
            else{
                lowBlocking = false;
            }

            if (inputDirection.In(4, 7))
            {
                hiBlocking = true;
            }
            else
            {
                hiBlocking = false;
            }
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
        Vector2 moveDir = Vector2.right;
        Debug.Log("Moving Forward " + forwardWalkSpeed);
        moveDir *= forwardWalkSpeed * Time.deltaTime * transform.lossyScale.x;
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
        Debug.Log("Moving Backward " + backWalkSpeed);
        moveDir *= backWalkSpeed * Time.deltaTime * transform.lossyScale.x;
        Debug.Log("After scale " + moveDir);
        rb.MovePosition(rb.position + moveDir);
        
        animator.SetBool("Crouch", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Back", true);
        animator.SetBool("Neutral", false);
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
        //SetAltColors(true);
        if (!canTheyDoStuff && !(activeMove ? activeMove.active : false)){
            //Debug.Log("P" + playerNum + " Light");
            foreach (FighterMove fm in moves)
            {
                //Debug.Log(fm.btn);
                if (fm.btn == FighterMove.AttackButton.L)
                {
                    //Debug.Log("Found a L move");
                    if (fm.inputDirection.Contains(inputDirection))
                    {
                        //Debug.Log("Found matching move");
                        movementSprites.SetActive(false);
                        activeMove = fm;
                        fm.StartMove();
                        return;
                    }
                }
            }
        }
    }

    public void OnHeavy()
    {
        if (!canTheyDoStuff && !(activeMove ? activeMove.active : false)){
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

    public void DoneMove()
    {
        activeMove = null;
        EndForcedAnim();
    }

    public void OnUniversal(){}

    public void OnSpecial(){}

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

    public void DisableActiveMove()
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
    }

    public void EnableHitstun(float stopDur, float stunDir)
    {
        DisableActiveMove();
        movementSprites.SetActive(false);
        hitStunObj.SetActive(true);
        audioSource.Stop();
        if(ouchAudioClips.Length > 0)
        {
            audioSource.clip = ouchAudioClips[new System.Random().Next(0, ouchAudioClips.Length)];
            audioSource.Play();
        }
        EnableForcedAnim(stopDur + stunDir);
    }

    public void playVictorySound(){
        if(victoryClips.Length > 0)
        {
            audioSource.clip = victoryClips[new System.Random().Next(0, victoryClips.Length)];
            audioSource.Play();
        }
    }
    
    public void EnableBlockstun(float stopDur, float stunDir)
    {
        DisableActiveMove();
        movementSprites.SetActive(false);
        blockStunObj.SetActive(true);
        EnableForcedAnim(stopDur + stunDir);
    }

    public void EnableVictory(float stopDur) {
        playVictorySound();
        EnableForcedAnim(stopDur);
    }

    public void EnableLoss(float stopDur) {
        EnableForcedAnim(stopDur);
    }

    public void EnableRoundStart(float stopDur){
        EnableForcedAnim(stopDur);
    }

    public void EnableForcedAnim(float _dur)
    {
        canTheyDoStuff = true;
        forcedAnimElapsed = 0;
        forcedAnimDuration = _dur;
    }

    public void IterateForcedAnim()
    {
        //forcedAnimElapsed += Math.Min(0.05f, Time.deltaTime * 60);
        //Debug.Log("DELTA " + Time.deltaTime * 60);
        forcedAnimElapsed += Time.deltaTime * 60;
        if(knockbackStrength != 0)
        {
            IterateKnockback();
        }
        //Debug.Log(hitstopFramesElapsed + " / " + hitstopDuration);
        if (forcedAnimDuration <= forcedAnimElapsed)
        {
            EndForcedAnim();
            knockbackStrength = 0;
        }
    }

    private void IterateKnockback()
    {
        Vector2 moveDir = Vector2.left;
        float lerped = (forcedAnimDuration-forcedAnimElapsed)/forcedAnimDuration;
        moveDir *= lerped * Time.deltaTime * transform.lossyScale.x;
        //Debug.Log("After scale " + moveDir);
        rb.MovePosition(rb.position + moveDir);
        
    }

    public void EndForcedAnim()
    {
        canTheyDoStuff = false;
        if (!activeMove) 
        {
            //Ending stun as receiving player
            hitStunObj.SetActive(false);
            blockStunObj.SetActive(false);
            movementSprites.SetActive(true);
        }
        else
        {
            //Ending hitstop as attacking player is done automatically
            
        }
    }

    public void SetAltColors(bool invert){
        float _invert = invert ? 1 : 0;
        SpriteRenderer[] renderers = this.GetComponentsInChildren<SpriteRenderer>(true);
        foreach(SpriteRenderer r in renderers)
        {
            r.material.SetFloat("_InvertColors", _invert);
        }
        foreach(AudioChorusFilter acf in GetComponentsInChildren<AudioChorusFilter>(true))
        {
            acf.enabled = invert;
        }
    }
}






