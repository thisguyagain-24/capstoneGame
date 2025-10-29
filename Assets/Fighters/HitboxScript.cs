using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    public FighterMove move;
    public MoveFrame frame;
    public BoxCollider2D _collider;
    public string hurtboxString;

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
        if (!_collider)
        {
            _collider = gameObject.GetComponent<BoxCollider2D>();
        }
        if(move?.GetComponentInParent<Fighter>().playerNum == 0)
        {
            hurtboxString = "Player2Hurtbox";
        }
        else
        {
            hurtboxString = "Player1Hurtbox";
        }
    }

    void Update()
    {
        if (frame)
        {
            if (frame.active)
            {
                foreach (Collider2D col in Physics2D.OverlapBoxAll(new Vector2(_collider.transform.position.x, _collider.transform.position.y) + (_collider.offset * _collider.transform.lossyScale), _collider.size * _collider.transform.lossyScale, _collider.transform.rotation.y, LayerMask.GetMask("Player2Hurtbox"))){
                    if (col)
                    {
                        SOMETHINGHAPPENED(col);
                        break;
                    }
                }
            }
        }
    }

    private void SOMETHINGHAPPENED(Collider2D col)
    {
        Debug.Log("SOMETHING");
        if (move)
        {
            Debug.Log("32");
            if (frame.active)
            {
                if (col.transform.parent.name == "Hitboxes")
                {
                    Debug.Log("CLASH");
                }

                string hitbox = concatParentNames(this.transform.name, this.transform);
                hitbox = string.Join("/", ReverseArr(hitbox.Split('/')));

                string hurtbox = concatParentNames(col.transform.name, col.transform);
                hurtbox = string.Join("/", ReverseArr(hurtbox.Split('/')));

                Debug.Log(hitbox + " HAS HIT " + hurtbox);
                move.processHit(frame);
            }
        }
    }

/*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("29");
        if (move)
        {
            Debug.Log("32");
            if (frame.active)
            {
                if (collision.transform.parent.name == "Hitboxes")
                {
                    Debug.Log("CLASH");
                }

                string hitbox = concatParentNames(this.transform.name, this.transform);
                hitbox = string.Join("/", ReverseArr(hitbox.Split('/')));

                string hurtbox = concatParentNames(collision.transform.name, collision.transform);
                hurtbox = string.Join("/", ReverseArr(hurtbox.Split('/')));

                Debug.Log(hitbox + " HAS HIT " + hurtbox);
                move.processHit(frame);
            }
        }
    }
*/
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
