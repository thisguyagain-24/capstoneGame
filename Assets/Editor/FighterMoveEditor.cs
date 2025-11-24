using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(FighterMove))]
// ^ This is the script we are making a custom editor for.
public class FighterMoveEditor : Editor {
    public override void OnInspectorGUI () {
    DrawDefaultInspector();
        if (GUILayout.Button("RefreshFrames"))
        {
            target.GetComponent<FighterMove>().GetFrames();
        }
    }
}