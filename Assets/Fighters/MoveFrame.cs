using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveFrame : MonoBehaviour
{
    [Min(1)]
    [Tooltip("Duration of move frame")]
    public int duration;
    public GameObject[] hitboxes;
    public GameObject[] hurtboxes;

    void Start()
    {
        if (hitboxes.Length == 0)
        {
            foreach (BoxCollider2D b in gameObject.transform.GetChild(0).GetComponentsInChildren<BoxCollider2D>())
            {
                hitboxes.Append(b.gameObject);
            }
        }
        if(hurtboxes.Length == 0)
        {
            foreach( BoxCollider2D b in gameObject.transform.GetChild(1).GetComponentsInChildren<BoxCollider2D>())
            {
                hurtboxes.Append(b.gameObject);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
