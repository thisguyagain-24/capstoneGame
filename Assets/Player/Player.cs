using System;
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
    public bool currentSide;
    public Fighter fighter;
    public double deadzone = 0.5;

    public TitleMenu titleMenu;
    public MainMenu mainMenu;
    public CharSelect charSelectMenu;
    public FightSceneManager fightSceneManager;

    public GameObject testKnight;
    public List<GameObject> characters;

    public string currControlSceheme;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        pInput ??= this.GetComponent<PlayerInput>();
        playerNum = pInput.playerIndex;

        Debug.Log("Player " + playerNum + " Joined with Control Scheme: " + pInput.currentControlScheme);
        //Debug.Log(pInput.devices.ToString());
        //Debug.Log(pInput.devices[0].displayName);

        FindUIDirector();
        DontDestroyOnLoad(this.GameObject());
        titleMenu?.Load();
    }

    public void FindUIDirector()
    {
        try {
            titleMenu = GameObject.Find("Canvas").GetComponent<TitleMenu>();
        } catch{ }
        try {
            mainMenu = GameObject.Find("EventSystem").GetComponent<MainMenu>();
        } catch { }
        try {
            charSelectMenu = GameObject.Find("CharSelect").GetComponent<CharSelect>();
        } catch { }  
        try {
            fightSceneManager = GameObject.Find("fightSceneManager").GetComponent<FightSceneManager>();
        } catch { }
    }

    // Update is called once per frame
    void Update()
    {
        currControlSceheme = pInput.currentActionMap.name;
    }

    #region GameplayInputs
    void OnMove(InputValue value)
    {
        Vector2 inVec = value.Get<Vector2>();
        Debug.Log("Player " + playerNum + " Input Move: " + inVec.ToString());
        if (!fighter.leftSide)
        {
            inVec.x *= -1;
        }
        fighter.OnMovementInput(GetDirection(inVec.x, inVec.y));
    }

    void OnLightAttack(InputValue value)
    {
        Debug.Log("Player " + playerNum + " Input Light: " + value.Get().ToString());
        fighter.OnLight();
    }

    void OnHeavyAttack(InputValue value)
    {
        Debug.Log("Player " + playerNum + " Input Heavy: " + value.Get().ToString());
        fighter.OnHeavy();
    }

    void OnUniversal(InputValue value)
    {
        Debug.Log("Player " + playerNum + " Input Universal: " + value.Get().ToString());
        fighter.OnUniversal();
        //pInput.SwitchCurrentActionMap("UI"); //Switching action maps for test
    }

    void OnSpecial(InputValue value)
    {
        Debug.Log("Player " + playerNum + " Input Special: " + value.Get().ToString());
        fighter.OnSpecial();
    }

    void OnPause(InputValue value)
    {
        Debug.Log("Player " + playerNum + " Input Pause: " + value.Get().ToString());
        Debug.Log(fightSceneManager);
        fightSceneManager?.PauseMenuHandler();
    }
    #endregion

    #region MenuInputs
    void OnNavigate(InputValue value)
    {
        Debug.Log("Player " + playerNum + " Input Navigate: " + value.Get().ToString());

        Vector2 readableValue = value.Get<Vector2>();

        int direction = GetDirection(readableValue.x, readableValue.y);

        if (playerNum == 0) {
            if (mainMenu)
            {
                switch (direction)
                {
                    case 2 or 8:
                        mainMenu?.MenuCursorUpDown();
                        break;
                    case 4 or 6:
                        mainMenu?.MenuCursorLeftRight();
                        break;
                }
            } 
        }
        if (charSelectMenu)
        {
            //Debug.Log(direction);
            charSelectMenu?.UpdateSelected(direction,playerNum);
        }
        if (fightSceneManager) {
            fightSceneManager?.PauseUpdateSelected(direction);
        }
    }

    void OnSubmit(InputValue value)
    {
        Debug.Log("Player " + playerNum + " Input Submit: " + value.Get().ToString());
        if (mainMenu) {
            mainMenu?.MenuCursorEnter();
        } else if (charSelectMenu) {
            Debug.Log("Player " + playerNum + " Has char select");
            charSelectMenu?.MenuCursorEnter(playerNum);
        } else if (fightSceneManager){
            fightSceneManager?.PauseCursorSelected();
        }
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

    void OnSpawnFighter(InputValue value)
    {
        MakeFighter();
    }
    #endregion

    public void MakeFighter() {
        if (!fighter)
        {
            fighter = Instantiate(characters[playerNum], transform).GetComponent<Fighter>();
            fighter.transform.SetParent(transform);
            fighter.transform.localScale = new Vector3(4,4,4);
            fighter.playerNum = playerNum;
            fighter.tag = "fighter";
        }
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

