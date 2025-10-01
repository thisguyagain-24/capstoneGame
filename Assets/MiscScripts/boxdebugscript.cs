using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxdebugscript : MonoBehaviour
{

    int ah;
    public BoxCollider2D _collider;
    // Start is called before the first frame update
    void Start()
    {
        ah = 0;
        //_collider ??= GetComponent<BoxCollider2D>();
        Gizmos.color = new Color(0.75f, 0.0f, 0.0f, 0.75f);
        Gizmos.matrix = transform.localToWorldMatrix;
    }

    [ExecuteAlways]
    void Update()
    {
        
            Debug.Log("IM HERE");
            _collider = gameObject.GetComponent<BoxCollider2D>();
            
    }

    void OnDrawGizmos()
    {
        _collider = gameObject.GetComponent<BoxCollider2D>();
            
        //Gizmos.DrawCube(transform.position, Vector3.one);
        Gizmos.DrawCube(transform.position + (Vector3)_collider.offset, _collider.size);
    }
}
