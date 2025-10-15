using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orion : Fighter
{
    public override double maxHealth { get; protected set; } = 100;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public override void die()
    {
        
    }

    public override void onLight()
    {
        throw new System.NotImplementedException();
    }

    public override void onHeavy()
    {
        throw new System.NotImplementedException();
    }

    public override void onUniversal()
    {
        throw new System.NotImplementedException();
    }

    public override void onSpecial()
    {
        throw new System.NotImplementedException();
    }

    public override void doneMove()
    {
        throw new System.NotImplementedException();
    }
}
