using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using JetBrains.Annotations;
using UnityEngine;


public class FightSceneManager : MonoBehaviour
{

    public Fighter[] fighters = new Fighter[2];

    public double[] fightersInitHealth = { 1, 1 };

    public double[] fightersCurrentHealth = { 1, 1 };

    public readonly int rounds = 2;

    public int roundsElapsed = 0;

    // Start is called before the first frame update
    void Start()
    {

        foreach (GameObject f in GameObject.FindGameObjectsWithTag("fighter"))
        {
            Fighter _f = f.GetComponent<Fighter>();
            fighters[_f.playerNum] = _f;
            Debug.Log("got fighter " + _f);
            _f.FindFightSceneManager();

        }

        for (int i = 0; i < fighters.Length; i++)
        {
            fightersInitHealth[i] = fighters[i].maxHealth;
        }




    }
// switch to getting fighter itself later
    public void PlayerDamageUpdate(double updatedHealth, int playerNum) {

        fightersCurrentHealth[playerNum] = updatedHealth;

        UpdateGUIHealth(playerNum);

    }

    public void UpdateGUIHealth(int playerNum) {

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
