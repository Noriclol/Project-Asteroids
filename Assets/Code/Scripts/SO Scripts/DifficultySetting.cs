using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Unnamed Difficulty", menuName = "ScriptableObjects/DifficultySettings")]
public class DifficultySetting : ScriptableObject
{
    public float AsteroidSpawnDistance = 20;

    public float AsteroidTargetSpawnRadius = 10;
    
    public float AsteroidTargetingOffset = 10;
}
