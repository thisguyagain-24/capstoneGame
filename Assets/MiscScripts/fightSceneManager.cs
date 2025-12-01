using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Unity.Burst;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;


public class FightSceneManager : MonoBehaviour
{
    public GameObject[] healthBarSprite = new GameObject[2];

    public GameObject[] healthBarOuter = new GameObject[2];

    public GameObject[] healthBarInner = new GameObject[2];

    public Fighter[] fighters = new Fighter[2];

    public readonly int rounds = 2;

    public int roundsElapsed = 0;

    public double healthBarScale;

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
        
        healthBarScale = healthBarSprite[0].transform.localScale.x;

        

    }
// switch to getting fighter itself later
    public void PlayerDamageUpdate(int playerNum) {

        UpdateGUIHealth(playerNum);

    }

    public void UpdateGUIHealth(int playerNum) {

        Transform barEnd = healthBarOuter[playerNum].transform;
        Transform barMid = healthBarSprite[playerNum].transform;

        double healthPercent = fighters[playerNum].currHealth / fighters[playerNum].maxHealth;

        barMid.localScale = new Vector3((float)healthPercent,1,1) * (float)healthBarScale;

        barEnd.localPosition = new Vector3(barMid.localPosition.x + barMid.GetComponent<SpriteRenderer>().bounds.size.x, barEnd.localPosition.y, barEnd.localPosition.z);

    }

    public void GuiHealthDie(int playerNum) {
        healthBarSprite[playerNum].SetActive(false);
        healthBarInner[playerNum].SetActive(false);
        healthBarOuter[playerNum].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
