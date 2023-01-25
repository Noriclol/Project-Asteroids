using System;
using System.Collections.Generic;
using PlasticGui;
using UnityEditor;
using UnityEngine;

public class GameEditor : EditorWindow
{
    // Fields Editor
    private string[] toolbarStrings = { "General", "SettingPresets", "Asteroids", "Ships" };
    private int toolbarSelection = 0;

    private string DifficultyPath = "Assets/Code/ScriptableObjects/Difficulty";
    
    
    
    
    // Difficulty Instantiation
    private string objectName = "new difficulty";

    
    private SerializedObject gameSettings;
    
    // Difficulty Instance Settings
    private SerializedProperty asteroidSpawnDistance;
    private SerializedProperty asteroidSpawnForce;
    private SerializedProperty spawnTimeInterval;
    private SerializedProperty asteroidsInCirculation;
    private SerializedProperty asteroids;
    private SerializedProperty targetMode;
    private SerializedProperty targetingSize;
    
    [MenuItem("Tools/Game Editor")]
    public static void ShowWindow()
    {
        var window = GetWindow<GameEditor>("CustomEditor");

        window.minSize = new Vector2(400, 200);

    }
    
    
    private void OnEnable()
    {
        gameSettings = new SerializedObject((DifficultySetting)Resources.Load("GameSettings"));
        
        asteroidSpawnDistance = gameSettings.FindProperty("AsteroidSpawnDistance");
        asteroidSpawnForce = gameSettings.FindProperty("AsteroidSpawnForce");
        spawnTimeInterval = gameSettings.FindProperty("SpawnTimeInterval");
        asteroidsInCirculation = gameSettings.FindProperty("AsteroidsInCirculation");
        asteroids = gameSettings.FindProperty("Asteroids");
        targetMode = gameSettings.FindProperty("targetMode");
        targetingSize = gameSettings.FindProperty("TargetingSize");
    }
    
    
    private void OnGUI()
    {
        EditorGUILayout.LabelField("Game Editor", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();
        GUILayout.Space(10);
        DisplaySettingsPanel();
        EditorGUILayout.EndVertical();
    }

    
    private void DisplaySettingsPanel()
    {
        gameSettings.Update();
        
        EditorGUILayout.BeginVertical("Box");

        EditorGUILayout.LabelField("Asteroid Settings", EditorStyles.boldLabel);
        
        GUILayout.Space(5);
        
        EditorGUILayout.PropertyField(asteroidSpawnDistance);
        EditorGUILayout.PropertyField(asteroidSpawnForce);
        EditorGUILayout.PropertyField(spawnTimeInterval);
        EditorGUILayout.PropertyField(asteroidsInCirculation);
        EditorGUILayout.PropertyField(asteroids);
        EditorGUILayout.PropertyField(targetMode);
        EditorGUILayout.PropertyField(targetingSize);
        EditorGUILayout.PropertyField(asteroidsInCirculation);

        GUILayout.Space(5);
        
        EditorGUILayout.EndVertical();
        
        gameSettings.ApplyModifiedProperties();
    }
}
