using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    public abstract double maxHealth { get; protected set; }
    private double incomingDamageModifier;

    public double health {get; protected set;}

    public FighterMove[] moves;

    public int maxLives;
    public int lives;

    public int maxTension;
    public int tension;

    public int maxBurst;
    public int burst;

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


    public abstract void die();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void subHealth(double damage){
        health = health - (damage * incomingDamageModifier);
    }
}
