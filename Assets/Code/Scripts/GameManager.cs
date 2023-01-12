using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    
    [SerializeField]
    private GameObject ShipPrefab;

    [SerializeField] 
    private GameObject AsteroidSmallPrefab;
    
    [SerializeField] 
    private GameObject AsteroidLargePrefab;


    private int asteroidCount = 5;

    private float asteroid_tourqeLimit = 5f;
    private float asteroid_speedLimit = 5f;
    
    
    private void Start()
    {
        //Spawn Player
        Instantiate(ShipPrefab, Vector3.zero, quaternion.identity);
        
        
        //Spawn Asteroids
        for (int i = 0; i < asteroidCount; i++)
        {
            SpawnAsteroid();
        }
    }


    private void SpawnAsteroid()
    {
        Vector3 newLocation = Random.insideUnitCircle * 5;

        
        
        
        
        var newStroid = Instantiate(AsteroidSmallPrefab, newLocation, quaternion.identity);

        newStroid.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-asteroid_tourqeLimit, asteroid_tourqeLimit));
        
        
        

    }


    private void FragmentAsteroid()
    {
        
    }
    
    
}
