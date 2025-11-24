using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Editor;

/// <summary>
/// You must make PlayerInputManagerEditor public rather than internal in order for this to work.
/// Inspector for <see cref="SharedDeviceInputManager"/>
/// Simply defers to <see cref="PlayerInputManagerEditor"/> in order to allow
/// default inspector capabilities.
/// </summary>
[CustomEditor(typeof(SharedDeviceInputManager))]
    public class SharedDeviceInputManagerEditor : UnityEditor.Editor
    {
        /// <summary>
        /// The <see cref="SharedDeviceInputManager"/> we are inspecting
        /// </summary>
        private SharedDeviceInputManager inputManager;
        
        /// <summary>
        /// The default editor for <see cref="PlayerInputManager"/>
        /// </summary>
        private UnityEditor.Editor defaultEditor;
        
        /// <summary>
        /// Initialise
        /// </summary>
        private void OnEnable()
        {
            inputManager = target as SharedDeviceInputManager;
            defaultEditor = UnityEditor.Editor.CreateEditor(inputManager as PlayerInputManager, typeof(PlayerInputManagerEditor));
        }

        /// <summary>
        /// Draw default editor
        /// </summary>
        public override void OnInspectorGUI()
        {
            defaultEditor.OnInspectorGUI();
        }
    }