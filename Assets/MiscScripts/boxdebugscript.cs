using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxdebugscript : MonoBehaviour
{
    public BoxCollider2D[] hitboxes;
    public BoxCollider2D[] hurtboxes;

    // Start is called before the first frame update
    void Start()
    {
        //_collider ??= GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
        
    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;

        hitboxes = gameObject.transform.GetChild(0).GetComponentsInChildren<BoxCollider2D>();
        hurtboxes = gameObject.transform.GetChild(1).GetComponentsInChildren<BoxCollider2D>();
        
        drawHitboxes();
        drawHurtboxes();
    }

    void drawHitboxes(){
        //Gizmos.color = new Color(0.8f, 0.05f, 0.05f, 0.2f);
        foreach (BoxCollider2D c in hitboxes)
        {
            //Debug.Log("DRAWING DRAWING HITBOXES");
            Gizmos.color = new Color(0.8f, 0.05f, 0.05f, 0.2f);
            Gizmos.DrawCube(c.transform.localPosition + (Vector3)c.offset, c.size);
            Gizmos.color = new Color(0.8f, 0.05f, 0.05f, 0.4f);
            Gizmos.DrawWireCube(c.transform.localPosition + (Vector3)c.offset, c.size);

        }
    }

    void drawHurtboxes()
    {
        //Gizmos.color = new Color(0.05f, 0.2f, 0.8f, 0.2f);
        foreach (BoxCollider2D c in hurtboxes)
        {
            //Debug.Log("DRAWING HURTBOXES");
            Gizmos.color = new Color(0.05f, 0.2f, 0.8f, 0.2f);
            Gizmos.DrawCube(c.transform.localPosition + (Vector3)c.offset, c.size);
            Gizmos.color = new Color(0.05f, 0.2f, 0.8f, 0.4f);
            Gizmos.DrawWireCube(c.transform.localPosition + (Vector3)c.offset, c.size);
        }
    }
}
