using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FighterMove : MonoBehaviour
{
    public int[] inputDirection;

/*
    public string inputsRaw;
    public string[] inputsSplit;
    public int[] inputsDir;
    public string[] inputsBtn;
*/
    public AttackButton btn;
    private Fighter fighter;

    [Min(0)]
    [Tooltip("Freeze frames when move connects")]
    public int hitstop;

    [Min(1)]
    [Tooltip("Frames enemy is stunned when hit")]
    public int hitstun;

    [Min(1)]
    [Tooltip("Frames enemy is stunned if blocked")]
    public int blockstun;

    public MoveFrame[] keys;

    public GameObject[] hitboxes;
    public GameObject[] hurtboxes;
    
    public int currentKey;
    public float framesElapsed;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        if (!inputDirection.Any(n => n >= 1 && n <= 9))
        {
            print("Move has no valid direction!");
        }
        active = false;
        fighter = this.GetComponentInParent<Fighter>();
        foreach (MoveFrame mf in keys)
        {
            foreach(GameObject o in mf.hitboxes)
            {
                o.layer = getPlayerHitboxLayer();
                hitboxes.Append(o);
            }
            foreach (GameObject o in mf.hurtboxes)
            {
                o.layer = fighter.getPlayerHurtboxLayer();
                hurtboxes.Append(o);
            }
            mf.gameObject.SetActive(false);
        }
    }

    private int getPlayerHitboxLayer()
    {
        if (fighter.playerNum == 0)
        {
            return LayerMask.NameToLayer("Player1Hitbox");
        }
        else
        {
            return LayerMask.NameToLayer("Player2Hitbox");
        }
    }

    public void StartMove()
    {
        if (!active)
        {
            currentKey = 0;
            framesElapsed = 0;
            active = true;
            keys[0].gameObject.SetActive(true);
            foreach (MoveFrame mf in keys)
            {
                mf.active = true;
            }
            Debug.Log("P" + fighter.playerNum + " STARTING " + transform.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active && !fighter.inForcedAnim)
        {
            IterateFrames();
        }
    }

    public void IterateFrames(){
        Debug.Log("Iterate P" + fighter.playerNum);
        framesElapsed += Time.deltaTime * 60;
        //Debug.Log(framesElapsed);
        if(keys[currentKey].duration <= framesElapsed)
        {
            framesElapsed -= keys[currentKey].duration;
            IterateKeys();
        }
    }

    public void IterateKeys()
    {
        keys[currentKey].gameObject.SetActive(false);
        currentKey++;
        Debug.Log("P" + fighter.playerNum + transform.parent.name + "   MOVING TO KEYFRAME " + currentKey);
        if(currentKey >= keys.Length)
        {
            fighter.DoneMove();
            active = false;
        }
        else
        {
            keys[currentKey].gameObject.SetActive(true);
        }
    }

    public void processHit(Hitbox hb)
    {
        impact(hb);
        if(fighter.opponent?.hiBlocking == false && fighter.opponent?.lowBlocking == false)
        {
            moveHit(hb);
        }
        else
        {
            if(hb.guard == Hitbox.guardType.low && fighter.opponent?.lowBlocking == false)
            {
                moveHit(hb);
            }
            else if(hb.guard == Hitbox.guardType.high && fighter.opponent?.hiBlocking == false)
            {
                moveHit(hb);
            }
            else
            {
                moveBlock(hb);
            }
        }  
    }

    public void impact(Hitbox hb)
    {
        fighter.EnableForcedAnim(hitstop);
        disableFrames(hb.frame.uniqueHitNumber);
    }

    public void moveHit(Hitbox hb)
    {
        fighter.opponent?.SubHealth(hb.damage);
        fighter.opponent?.EnableHitstun(hitstop, hitstun);
    }

    public void moveBlock(Hitbox hb)
    {
        fighter.opponent?.EnableBlockstun(hitstop, blockstun);
    }
    
    private void disableFrames(int hitNum)
    {
        foreach(MoveFrame mf in gameObject.GetComponentsInChildren<MoveFrame>(true).Where(mf => mf.uniqueHitNumber == hitNum))
        {
            Debug.Log(mf.name + " getting disabled");
            mf.active = false;
        }
    }

    public void GetFrames()
    {
        keys = this.GetComponentsInChildren<MoveFrame>();
    }

    public enum AttackButton
    {
        L,
        H,
        U,
        S
    }
}
