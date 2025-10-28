using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    public FighterMove move;
    public MoveFrame frame;

    void Start()
    {
        if (!move)
        {
            move = gameObject.GetComponentInParent<FighterMove>();
        }
        if (!frame)
        {
            frame = gameObject.GetComponentInParent<MoveFrame>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /* 
        This will always be true FOR CURRENT MOVES.
        Entities such as projectiles or summons may be different?
        */
        if (move)
        {
            if (frame.active)
            {
                string hitbox = concatParentNames(this.transform.name, this.transform);
                hitbox = string.Join("/", ReverseArr(hitbox.Split('/')));

                string hurtbox = concatParentNames(collision.transform.name, collision.transform);
                hurtbox = string.Join("/", ReverseArr(hurtbox.Split('/')));

                Debug.Log(hitbox + " HAS HIT " + hurtbox);
                move.processHit(frame);
            }
        }
    }

    private string concatParentNames(string name, Transform t)
    {
        if (t.parent)
        {
            return concatParentNames(name + "/" + t.parent.name, t.parent);
        }
        else
        {
            return name;
        }
    }
    
    private string[] ReverseArr(string[] str)
    {
        Array.Reverse(str);
        return str;
    }
}
