using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Burst;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;


public class FightSceneManager : MonoBehaviour
{
    public GameObject[] healthBarSprite = new GameObject[2];

    public GameObject[] healthBarOuter = new GameObject[2];

    public Fighter[] fighters = new Fighter[2];

    public readonly int rounds = 2;

    public int roundsElapsed = 0;

    public Vector3 healthBarScale;

    public bool pause = false;

    // Start is called before the first frame update
    void Start(){

        foreach (GameObject f in GameObject.FindGameObjectsWithTag("fighter"))
        {
            Fighter _f = f.GetComponent<Fighter>();
            fighters[_f.playerNum] = _f;
            Debug.Log("got fighter " + _f);
            _f.FindFightSceneManager();

        }

        healthBarScale = healthBarSprite[0].transform.localScale;
    }

#region GameplayUI
    public double CalcGuiHealth(int _fighter) {
        return fighters[_fighter].currHealth - (fighters[_fighter].maxHealth / 20);
    }
// switch to getting fighter itself later
    public void PlayerDamageUpdate(int playerNum) {
        UpdateGUIHealth(playerNum);
    }

    public void UpdateGUIHealth(int playerNum) {

        Transform barEnd = healthBarOuter[playerNum].transform;
        Transform barMid = healthBarSprite[playerNum].transform;

        double healthPercent = CalcGuiHealth(playerNum) / fighters[playerNum].maxHealth;

        Debug.Log(CalcGuiHealth(playerNum));
        Debug.Log(healthPercent);

        if (healthPercent<=0){
            barMid.localScale = new Vector3((float)healthPercent * (float)0,healthBarScale.y,healthBarScale.z);
        } else {
            barMid.localScale = new Vector3((float)healthPercent * (float)healthBarScale.x,healthBarScale.y,healthBarScale.z);
        }

        barEnd.localPosition = new Vector3(barMid.localPosition.x + barMid.GetComponent<SpriteRenderer>().bounds.size.x, barEnd.localPosition.y, barEnd.localPosition.z);

    }

    public void GuiHealthDie(int playerNum) {
        healthBarSprite[playerNum].SetActive(false);
        healthBarOuter[playerNum].SetActive(false);
    }

#endregion

#region Menu

    public void PauseMenuHandler() {

        pause = !pause;
        
        if(pause){
            Debug.Log("PAUSE");
            Time.timeScale = 0;
        }

    }

#endregion
    // Update is called once per frame
    void Update()
    {
        
    }
}
