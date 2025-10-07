using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxdebugscript : MonoBehaviour
{

    int ah;
    public BoxCollider2D[] colliders;
    public BoxCollider2D[] hitboxes;
    public BoxCollider2D[] hurtboxes;

    // Start is called before the first frame update
    void Start()
    {
        ah = 0;
        //_collider ??= GetComponent<BoxCollider2D>();
        Gizmos.matrix = transform.localToWorldMatrix;
    }

    void Update()
    {
        
    }

    void OnDrawGizmos()
    {

        Debug.Log("IM HERE");
        colliders = gameObject.transform.GetChild(0).GetComponentsInChildren<BoxCollider2D>();
        hitboxes = gameObject.transform.GetChild(1).GetComponentsInChildren<BoxCollider2D>();
        hurtboxes = gameObject.transform.GetChild(2).GetComponentsInChildren<BoxCollider2D>();

        drawColliders();
        drawHitboxes();
        drawHurtboxes();
    }

    void drawColliders()
    {
        Gizmos.color = new Color(1f, 1f, .0f, 0.2f);
        foreach(BoxCollider2D c in colliders) {
            Debug.Log("DRAWING COLLIDER");
            Gizmos.DrawCube(c.transform.position + (Vector3)c.offset, c.size);
        }
    }

    void drawHitboxes(){
        Gizmos.color = new Color(0.6f, 0.0f, 0.0f, 0.2f);
        foreach(BoxCollider2D c in hitboxes) {
            Debug.Log("DRAWING DRAWING HITBOXES");
            Gizmos.DrawCube(c.transform.position + (Vector3)c.offset, c.size);
        }
    }

    void drawHurtboxes()
    {
        Gizmos.color = new Color(0f, 0.0f, 0.6f, 0.2f);
        foreach(BoxCollider2D c in hurtboxes) {
            Debug.Log("DRAWING HURTBOXES");
            Gizmos.DrawCube(c.transform.position + (Vector3)c.offset, c.size);
        }
    }
}
