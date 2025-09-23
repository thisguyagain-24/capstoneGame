using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //public SOMETHING controller?
    public int inputNumber;
    public bool startSide;
    public Fighter character;

    public PlayerInput pInput;
    public int playerNum = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (pInput is null)
        {
            pInput = this.GetComponent<PlayerInput>();
        }
        playerNum = pInput.playerIndex;
        Debug.Log("Player " + playerNum + " Joined with Control Scheme: " + pInput.currentControlScheme);
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

    }
    #endregion

}
