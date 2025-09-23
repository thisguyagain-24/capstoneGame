using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// reusable ui things like transitions between scenes

public class UiTools : MonoBehaviour
{
    public GameObject FadeRect;

    public void FadeToBlack()
    {
        GameObject thisRect = Instantiate(FadeRect);

        // todo: do it


    }

    public void FadeFromBlack()
    {
        
    }
}
