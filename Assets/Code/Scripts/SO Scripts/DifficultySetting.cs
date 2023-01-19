using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unnamed Difficulty", menuName = "ScriptableObjects/DifficultySettings")]
public class DifficultySetting : ScriptableObject
{
    [Header("Spawning")]
    public float AsteroidSpawnDistance = 20;

    public float AsteroidSpawnForce = 100;

    public float SpawnTimeInterval = 3;

    [Header("Asteroids")] 
    
    public int AsteroidsInCirculation = 10;
    
    public List<Asteroid> Asteroids;
    

    [Header("Targeting")] 
    public TargetingMode targetMode;
    
    public float AsteroidTargetingSize = 10;

    
    

    public enum TargetingMode
    {
        Center,
        General,
        Player,
    }
}
