using System;
using PlasticGui;
using UnityEditor;
using UnityEngine;

public class GameEditor : EditorWindow
{
    private string[] toolbarStrings = { "General", "SettingPresets", "Asteroids", "Ships" };
    private int toolbarSelection = 0;
    
    
    
    [MenuItem("Tools/Game Editor")]
    public static void ShowWindow()
    {
        GetWindow<GameEditor>("CustomEditor");
    }
    private void OnGUI()
    {
        
        GUILayout.BeginHorizontal();
        toolbarSelection = GUILayout.Toolbar(toolbarSelection, toolbarStrings);
        GUILayout.EndHorizontal();

        switch (toolbarSelection)
        {
            case 0:
                break;
            case 1:
                GUILayout.Space(20);
                GUILayout.Label("New Difficulty", EditorStyles.toolbar);
                break;
            case 2:
                break;
            case 3:
                break;
        }
        
        

        
        //editor code
    }
}
