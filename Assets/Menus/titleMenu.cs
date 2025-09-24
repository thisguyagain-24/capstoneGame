// script for title menu / start screen, it just listens for any input and then changes scene if it catches smth

using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    private bool keyPressed = false;

    void Start()
    {
        
    }

    void Update()
    {
        /*
        if (!keyPressed && Input.anyKeyDown)
        {
            keyPressed = true;

            SceneManager.LoadScene("mainMenu");
        }
        */
    }

    public void doIt()
    {
        SceneManager.LoadScene("mainMenu");

    }
}