using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject MainLogo;

    [SerializeField]
    public GameObject TapeLogo;

    [SerializeField]
    public GameObject SubLogo;

    public double MainFramesToAnimationEnd = 45;

    public double TapeFramesToAnimationEnd = 30;

    public double SubFramesToAnimationEnd = 30;

    public int FrameCount = 0;

    public bool AnimationDone = false;

    public void Load(){

        SceneManager.LoadScene("mainMenu");
    }

    public void Start() {

    }

    private void Update()
    {
        if (!AnimationDone)
        {
            DoAnimation();
        }

    }

    private void DoAnimation() {

        if (MainFramesToAnimationEnd > 0) {

            MainLogo.transform.localPosition = Vector3.Lerp(MainLogo.transform.localPosition, new Vector3(-4, 173, 0), 0.1f);

            MainFramesToAnimationEnd -= Time.deltaTime / 0.02;

            return;
        }

        TapeLogo.transform.localPosition = new Vector3 (-6, 184, 1);

        if (TapeFramesToAnimationEnd > 0) {

            TapeLogo.transform.localScale = Vector3.Lerp(TapeLogo.transform.localScale, new Vector3(32,32,32), 0.8f);

            TapeFramesToAnimationEnd-= Time.deltaTime / 0.02;

            return;
        }

        if (SubFramesToAnimationEnd > 0) {

            SubLogo.transform.localPosition = Vector3.Lerp(SubLogo.transform.localPosition, new Vector3(-10,0,1), 0.1f);

            SubFramesToAnimationEnd-= Time.deltaTime / 0.02;

            return;
        }

        AnimationDone = true;
    }
}