using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public double maxHealth { get; private set; } = 100;

    private double _health;

    [SerializeField]
    public double health
    {
        get => _health;
        set
        {
            _health = value;
            if (_health <= 0)
            {
                die();
            }
            if (_health > maxHealth) {
                _health = maxHealth;
            }
        }
    }

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
    
    public void die()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
