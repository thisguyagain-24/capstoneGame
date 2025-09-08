using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public static readonly State NEUTRAL = new State("Neutral", "5", StateType.Position);
    public static readonly State CROUCH = new State("Crouch", "2", StateType.Position);
    public static readonly State BACKDASH = new State("Backdash");
    public static readonly State PREJUMP = new State("Prejump");
    public static readonly State AIRBORNE = new State("Airborne");
    public static readonly State BLOCKING = new State("Blocking", "4", StateType.Position);
    public static readonly State HITSTOP = new State("Hitstop", StateType.Control);
    public static readonly State BLOCKSTUN = new State("Blockstun", StateType.Control);
    public static readonly State HITSTUN = new State("Hitstun", StateType.Control);
    public static readonly State STARTUP = new State("Startup", StateType.Control);
    public static readonly State ACTIVE = new State("Active");
    public static readonly State RECOVERY = new State("Recovery");
    public static readonly State GATLING_LIGHT = new State("Light Recovery");
    public static readonly State GATLING_HEAVY = new State("Heavy Recovery");
    public static readonly State LOCKED = new State("Locked");
    public static readonly State KNOCKDOWN_SOFT = new State("Soft Knockdown", "SKD");
    public static readonly State KNOCKDOWN_HARD = new State("Hard Knockdown", "HKD");
    

    public string name { get; private set; }
    public string sname {get; private set;}
    public StateType type { get; private set; }

    State(string _name, StateType _type) => (name, type) = (_name, _type);
    State(string _name) => name = _name;
    State(string _name, string _sname) => (name, sname) = (_name, _sname);
    State(string _name, string _sname, StateType _type) => (name, sname, type) = (_name, _sname, _type);
    public enum StateType
    {
        Position,
        Control,
        Effect,
        Stance
    }
}
