using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerInput pInput;
    public int playerNum = -1;

    public int inputDirection;

    public bool startSide;
    public Fighter character;
    public double deadzone = 0.5;

    public TitleMenu t;
    public MainMenu m;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        pInput ??= this.GetComponent<PlayerInput>();
        playerNum = pInput.playerIndex;
        
        Debug.Log("Player " + playerNum + " Joined with Control Scheme: " + pInput.currentControlScheme);
        FindUIDirector();
        DontDestroyOnLoad(this.GameObject());
    }

    public void FindUIDirector()
    {
        try
        {
            t = GameObject.Find("Canvas").GetComponent<TitleMenu>();
        }
        catch{}
        try
        {
            m = GameObject.Find("EventSystem").GetComponent<MainMenu>();
        }
        catch { }
        }

    // Update is called once per frame
    void Update()
    {

    }

    #region GameplayInputs
    void OnMove(InputValue value)
    {
        Debug.Log("Player " + playerNum + " Input Move: " + value.Get().ToString());
        
    }

    void OnLightAttack(InputValue value)
    {
        Debug.Log("Player " + playerNum + " Input Light: " + value.Get().ToString());

    }

    void OnHeavyAttack(InputValue value)
    {
        Debug.Log("Player " + playerNum + " Input Heavy: " + value.Get().ToString());

    }

    void OnUniversal(InputValue value)
    {
        Debug.Log("Player " + playerNum + " Input Universal: " + value.Get().ToString());
        //pInput.SwitchCurrentActionMap("UI"); //Switching action maps for test
    }

    void OnSpecial(InputValue value)
    {
        Debug.Log("Player " + playerNum + " Input Special: " + value.Get().ToString());

    }

    void OnPause(InputValue value)
    {
        Debug.Log("Player " + playerNum + " Input Pause: " + value.Get().ToString());

    }
    #endregion

    #region MenuInputs
    void OnNavigate(InputValue value)
    {
        Debug.Log("Player " + playerNum + " Input Navigate: " + value.Get().ToString());

    }

    void OnSubmit(InputValue value)
    {
        Debug.Log("Player " + playerNum + " Input Submit: " + value.Get().ToString());

    }

    void OnCancel(InputValue value)
    {
        Debug.Log("Player " + playerNum + " Input Cancel: " + value.Get().ToString());
        //pInput.SwitchCurrentActionMap("Player"); //Switching action maps for test
        t?.doIt(); 
    }
    #endregion

    private int ProcessInput(Vector2 input)
    {
        if (input.magnitude < deadzone)
        {
            return 5;
        }

        if (pInput.devices[0].name == "Keyboard")
        {

        }

        return 0;
    }

}
