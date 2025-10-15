using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Unity.VisualScripting.FullSerializer;


public class TestKnight : Fighter
{
    public override double maxHealth { get; protected set; } = 100;

    // Start is called before the first frame update
    void Start()
    {
        incomingDamageModifier = 0.9;
        GetMoves();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public override void die()
    {

    }

    public override void onMove(int dir)
    {
        inputDirection = dir;
        Debug.Log("KNIGHT MOVING WITH " + dir);
        if (dir == 5)
        {
            //this is bad dont do this
            animator.SetBool("Crouch", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Back", false);
            animator.SetBool("Neutral", true);

        }
        if (dir.In(1, 2, 3))
        {
            animator.SetBool("Crouch", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Back", false);
            animator.SetBool("Neutral", false);
        }
        else if (dir.In(3, 6, 9))
        {
            animator.SetBool("Crouch", false);
            animator.SetBool("Walk", true);
            animator.SetBool("Back", false);
            animator.SetBool("Neutral", false);
        }
        else if (dir.In(1, 4, 7))
        {
            animator.SetBool("Crouch", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Back", true);
            animator.SetBool("Neutral", false);
        }

    }

    public void GetMoves()
    {
        moves = this.GetComponentsInChildren<FighterMove>();
    }

    public override void onLight()
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

    public override void doneMove()
    {
        movementSprites.SetActive(true);
    }

    public override void onHeavy()
    {
        
    }

    public override void onUniversal()
    {
        
    }

    public override void onSpecial()
    {
        
    }
}

[CustomEditor(typeof(TestKnight))]
// ^ This is the script we are making a custom editor for.
public class TestKnightEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("RefreshMoves"))
        {
            target.GetComponent<TestKnight>().GetMoves();
        }
    }
}


