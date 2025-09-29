// script for title menu / start screen, it just listens for any input and then changes scene if it catches smth

using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject MainLogo;

    [SerializeField]
    public GameObject TapeLogo;

    [SerializeField]
    public GameObject TapeCanvas;

    [SerializeField]
    public GameObject SubLogo;

    public void Load(){

        SceneManager.LoadScene("mainMenu");
    }

    public void Start() {

        doAnimation();
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

    private void doAnimation() {

        MainLogo.transform.localPosition = new Vector3(0,0,0);


    }
}