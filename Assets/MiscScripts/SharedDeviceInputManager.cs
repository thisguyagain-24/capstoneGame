using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A PlayerInputManager that allows multiple users to share a keyboard
/// </summary>
public class SharedDeviceInputManager : UnityEngine.InputSystem.PlayerInputManager
{
    /// <summary>
    /// Replacement for <see cref="UnityEngine.InputSystem.PlayerInputManager.JoinPlayerFromActionIfNotAlreadyJoined"/>,
    /// allowing <see cref="SharedDeviceInputManager"/> to let players share a keyboard
    /// which splits keys into separate control schemes. You must make base.JoinPlayerFromActionIfNotAlreadyJoined virtual
    /// and base.CheckIfPlayerCanJoin protected
    /// </summary>
    /// <param name="context">The input action's callback context data</param>
    public override void JoinPlayerFromActionIfNotAlreadyJoined(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!CheckIfPlayerCanJoin())
            return;

        var device = context.control.device;
 
        /*
         * We want to allow sharing of keyboard devices, so keyboard devices
         * don't need to be checked here
         */

        if (device is not Keyboard)
        {
            if (PlayerInput.FindFirstPairedToDevice(device) != null)
                return;
        }

        var p = JoinPlayer(pairWithDevice: device);

        /*
         * We also want to make sure players sharing the keyboard have a unique
         * control scheme, so we call RebindPlayer to set that up
         */
        if (device is Keyboard)
        {
            RebindPlayer(p);
        }
    }

    #region ...
    /// <summary>
    /// Names of the control schemes we'll be using
    /// </summary>
    private string[] controlSchemes = new[]
    {
            "WASD", "Arrows"
        };

    /// <summary>
    /// Simple player index tracker, so we can assign different control schemes to different players
    /// </summary>
    private int playerIndex = 0;

    /// <summary>
    /// Set the player's control scheme based on their index
    /// </summary>
    /// <param name="obj"></param>
    private void RebindPlayer(UnityEngine.InputSystem.PlayerInput obj)
    {
        obj.SwitchCurrentControlScheme(controlSchemes[playerIndex], Keyboard.current);
        playerIndex++;
    }
    #endregion
}