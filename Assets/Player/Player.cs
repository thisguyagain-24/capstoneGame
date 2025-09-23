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
        Debug.Log(playerNum);
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region GameplayInputs
    void OnMove()
    {

    }

    void OnLight()
    {

    }

    void OnHeavy()
    {

    }

    void OnUniversal()
    {

    }

    void OnSpecial()
    {

    }

    void OnPause()
    {

    }
    #endregion

    #region MenuInputs
    void OnNavigate()
    {

    }

    void OnSubmit()
    {

    }

    void OnCancel()
    {
        
    }
    #endregion

}
