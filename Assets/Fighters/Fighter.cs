using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using Unity.VisualScripting;
using UnityEditor;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using System;

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

    public Animator animator;

    public int inputDirection;
    public bool inAir;
    public bool crouching;
    public bool neutral;
    public bool startup;
    public bool active;
    public bool recovery;
    public bool softKnockdown;
    public bool hardKnockdown;
    public bool hitstun;
    public bool blockstun;
    public bool leftSide;

    public GameObject movementSprites;
    public FighterMove[] moves;

    public void die()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        inputDirection = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(inputDirection == 0)
        {
            Debug.LogError("P" + playerNum + " HAS INVALID INPUT DIRECTION");           
        }
        if (inputDirection == 5)
        {
            //this is bad dont do this
            animator.SetBool("Crouch", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Back", false);
            animator.SetBool("Neutral", true);
        }
        else
        {
            Debug.Log("KNIGHT MOVING WITH " + inputDirection);
        }
        if (inputDirection.In(1, 2, 3))
        {
            animator.SetBool("Crouch", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Back", false);
            animator.SetBool("Neutral", false);
        }
        else if (inputDirection.In(3, 6, 9))
        {
            animator.SetBool("Crouch", false);
            animator.SetBool("Walk", true);
            animator.SetBool("Back", false);
            animator.SetBool("Neutral", false);
        }
        else if (inputDirection.In(1, 4, 7))
        {
            animator.SetBool("Crouch", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Back", true);
            animator.SetBool("Neutral", false);
        }
    }

    public void subHealth(double damage)
    {
        currHealth = currHealth - (damage * incomingDamageModifier);
    }

    public void onMove(int dir)
    {
        inputDirection = dir;
        

    }

    public void GetMoves()
    {
        moves = this.GetComponentsInChildren<FighterMove>();
    }

    public void onLight()
    {
        Debug.Log("TK Light");
        foreach (FighterMove fm in moves)
        {
            if (fm.btn == FighterMove.AttackButton.L)
            {
                Debug.Log("Found a L move");
                if (fm.inputDirection.Contains(inputDirection))
                {
                    Debug.Log("Found matching move");
                    movementSprites.SetActive(false);
                    fm.StartMove();
                }
            }
        }
    }

    public void doneMove()
    {
        movementSprites.SetActive(true);
    }

    public void onHeavy()
    {
        
    }

    public void onUniversal()
    {
        
    }

    public void onSpecial()
    {
        
    }
}

[CustomEditor(typeof(Fighter))]
// ^ This is the script we are making a custom editor for.
public class FighterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("RefreshMoves"))
        {
            target.GetComponent<Fighter>().GetMoves();
        }
    }
}

