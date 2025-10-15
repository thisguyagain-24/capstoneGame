using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FighterMove : MonoBehaviour
{
    public int[] inputDirection;
    public string inputs;
    public AttackButton btn;

    [Min(0)]
    [Tooltip("Freeze frames when move connects")]
    public int hitstop;

    [Min(1)]
    [Tooltip("Frames enemy is stunned when hit")]
    public int hitstun;

    [Min(1)]
    [Tooltip("Frames enemy is stunned if blocked")]
    public int blockstun;

    public MoveFrame[] frames;

    // Start is called before the first frame update
    void Start()
    {
        if (inputDirection.Any(n => n >= 1 && n <= 9))
        {
            print("Move has no valid direction!");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void getFrames()
    {

        frames = this.GetComponentsInChildren<MoveFrame>();
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
        if (GUILayout.Button("RefreshMoves"))
        {
            target.GetComponent<FighterMove>().getFrames();
        }
    }
}