using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(Fighter))]
// ^ This is the script we are making a custom editor for.
public class FighterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("RefreshMoves"))
        {
            target.GetComponent<Fighter>().GetMoves();
        }
    }
}