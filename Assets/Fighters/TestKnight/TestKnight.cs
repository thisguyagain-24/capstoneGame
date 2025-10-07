using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestKnight : Fighter
{
    public override double maxHealth { get; protected set; } = 100;

    // Start is called before the first frame update
    void Start()
    {
        incomingDamageModifier = 0.9;
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
}
