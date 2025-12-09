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
    public int uniqueHitNumber;
    public bool active;

    public  AudioSource audioSource;
    public AudioClip[] audioClips;

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

    public void playHitSound(System.Random rand)
    {
        if(audioClips.Length > 0)
        {
            audioSource.clip = audioClips[rand.Next(0, audioClips.Length)];
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
