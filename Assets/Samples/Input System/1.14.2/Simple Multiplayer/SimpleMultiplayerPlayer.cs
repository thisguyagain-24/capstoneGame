using UnityEngine;
using System.Reflection;
 
public class SimpleMultiplayerPlayer : MonoBehaviour
{
    // Just add *some* kind of movement. The specifics here are not of interest. Serves just to
    // demonstrate that the inputs are indeed separate.
    public int playerNum;
    public UnityEngine.InputSystem.PlayerInput input;


    public void OnTeleport()
    {
        transform.position = new Vector3(Random.Range(-75, 75), 0.5f, Random.Range(-75, 75));
        Debug.Log(input.playerIndex);
    }

}
