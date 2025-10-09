using System;
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
    public Fighter fighter;
    public double deadzone = 0.5;

    public TitleMenu t;
    public MainMenu m;

    public GameObject testKnight;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        pInput ??= this.GetComponent<PlayerInput>();
        playerNum = pInput.playerIndex;

        Debug.Log("Player " + playerNum + " Joined with Control Scheme: " + pInput.currentControlScheme);
        Debug.Log(pInput.devices.ToString());
        Debug.Log(pInput.devices[0].displayName);


        FindUIDirector();
        DontDestroyOnLoad(this.GameObject());
        t?.Load();
    }

    public void FindUIDirector()
    {
        try
        {
            t = GameObject.Find("Canvas").GetComponent<TitleMenu>();
        }
        catch { }
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
        Vector2 FUCK = value.Get<Vector2>();
        Debug.Log("Player " + playerNum + " Input Move: " + FUCK.ToString());
        fighter.onMove(GetDirection(FUCK.x, FUCK.y));
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

        Vector2 readableValue = value.Get<Vector2>();

        int direction = GetDirection(readableValue.x, readableValue.y);

        if (playerNum == 0) {

            if (m is not null) {

                switch (direction) {

                    case 2 or 8:

                        m.MenuCursorUpDown();

                        break;

                    case 4 or 6:

                        m.MenuCursorLeftRight();

                        break;

                }


            }

        }

    }

    void OnSubmit(InputValue value)
    {
        Debug.Log("Player " + playerNum + " Input Submit: " + value.Get().ToString());

        m?.MenuCursorEnter();

    }

    void OnCancel(InputValue value)
    {
        Debug.Log("Player " + playerNum + " Input Cancel: " + value.Get().ToString());
        //pInput.SwitchCurrentActionMap("Player"); //Switching action maps for test
    }

    void OnSwitchInput(InputValue value) {
        if (pInput.currentActionMap.name == "Player")
        {
            Debug.Log("Player " + playerNum + "switching to UI input mode");
            pInput.SwitchCurrentActionMap("UI");
        }
        else
        {
            Debug.Log("Player " + playerNum + "switching to Fighter input mode");
            pInput.SwitchCurrentActionMap("Player");
        }
    }

    void OnSpawnKnight(InputValue value)
    {
        if (!fighter)
        {

            fighter = Instantiate(testKnight, this.transform).GetComponent<TestKnight>();
        }
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

    public int GetDirection(float x, float y) {
    double absX = Math.Abs(x);
    double absY = Math.Abs(y);
    if (absX < deadzone && absY < deadzone) {
        // close to center
        return 5;
    }
    if (absX > absY) {
        // vertical side
        double half = absX * 0.4142;
        if (x < 0) {
            // left side
            if (y > half) return 1;
            if (y < -half) return 7;
            return 4;
        } else {
            // right side
            if (y > half) return 3;
            if (y < -half) return 9;
            return 6;
        }
    } else {
        // horisontal side
        double half = absY * 0.4142;
        if (y < 0) {
            // bottom
            if (x > half) return 3;
            if (x < -half) return 1;
            return 2;
        } else {
            // top
            if (x > half) return 9;
            if (x < -half) return 7;
            return 8;
        }
    }
    }
}

