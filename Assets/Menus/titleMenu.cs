// script for title menu / start screen, it just listens for any input and then changes scene if it catches smth

using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public void Load(){

        SceneManager.LoadScene("mainMenu");
    }

    private void Update()
    {  
        /* old bad code
        if (!keyPressed && Input.anyKeyDown)
        {
            keyPressed = true;

            SceneManager.LoadScene("mainMenu");
        }
        */
    }
}