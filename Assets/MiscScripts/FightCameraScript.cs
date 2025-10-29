using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCameraScript : MonoBehaviour
{
    public Fighter[] fighters = new Fighter[2];
    public Vector3 midpoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!fighters[0])
        {
            foreach (Fighter f in GameObject.FindObjectsByType<Fighter>(FindObjectsSortMode.None))
            {
                fighters[f.playerNum] = f;
            }
        }
        else if(fighters[1])
        {
            midpoint = fighters[1].transform.position + ((fighters[0].transform.position - fighters[1].transform.position) / 2);
            midpoint.z = -10f;
            transform.SetPositionAndRotation(Vector3.Lerp(transform.position, midpoint, 0.05f), transform.rotation);
        }
    }
}
