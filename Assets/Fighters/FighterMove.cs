using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
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
            foreach (BoxCollider2D box in mf.gameObject.transform.GetChild(0).GetComponentsInChildren<BoxCollider2D>())
            {
                hitboxes.Append(box.gameObject);
            }
            foreach (BoxCollider2D box in mf.gameObject.transform.GetChild(1).GetComponentsInChildren<BoxCollider2D>()) 
            {
                hurtboxes.Append(box.gameObject);
            }
            mf.gameObject.SetActive(false);
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
            Debug.Log("STARTING");

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            IterateFrames();
        }
    }
    
    public void IterateFrames(){
        framesElapsed += Time.deltaTime*60;
        Debug.Log(framesElapsed);
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
        Debug.Log("MOVING TO KEYFRAME " + currentKey);
        if(currentKey >= keys.Length)
        {
            fighter.doneMove();
            active = false;
        }
        else
        {
            keys[currentKey].gameObject.SetActive(true);
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

[CustomEditor(typeof(FighterMove))]
// ^ This is the script we are making a custom editor for.
public class FighterMoveEditor : Editor {
    public override void OnInspectorGUI () {
    DrawDefaultInspector();
        if (GUILayout.Button("RefreshFrames"))
        {
            target.GetComponent<FighterMove>().GetFrames();
        }
    }
}