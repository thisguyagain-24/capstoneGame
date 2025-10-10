using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushboxDebugScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        BoxCollider2D pushbox = GetComponent<BoxCollider2D>();
        Gizmos.color = new Color(1f, 1f, .0f, 0.2f);
        Gizmos.DrawCube(this.transform.localPosition + (Vector3)pushbox.offset, pushbox.size);
        Gizmos.color = new Color(1f, 1f, .0f, 0.4f);
        Gizmos.DrawWireCube(this.transform.localPosition + (Vector3)pushbox.offset,pushbox.size);
    }
}
