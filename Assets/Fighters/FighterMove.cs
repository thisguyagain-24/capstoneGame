using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterMove : MonoBehaviour
{
    public int startup;
    public int active;
    public int recovery;

    public int direction;
    public AttackButton btn;

    // Start is called before the first frame update
    void Start()
    {
        if (direction <= 0)
        {
            print("Move has no direction!");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public enum AttackButton
    {
        L,
        H,
        U,
        S
    }
}
