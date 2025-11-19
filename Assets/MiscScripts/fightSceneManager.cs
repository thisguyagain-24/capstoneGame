using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;



public class FightSceneManager : MonoBehaviour
{

    public Fighter[] fighters = new Fighter[2];

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


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
