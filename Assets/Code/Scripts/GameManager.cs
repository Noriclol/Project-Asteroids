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

    //[SerializeField] 
    //private GameObject AsteroidHandlePrefab;
    
    [SerializeField] 
    private int asteroidCount = 5;

    [SerializeField] 
    private float asteroid_tourqeLimit = 5f;
    
    [SerializeField] 
    private float asteroid_speedLimit = 5f;
    

    public List<Asteroid> AsteroidTypes = new List<Asteroid>();

    public List<AsteroidHandle> Asteroids = new List<AsteroidHandle>();
    
    
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


    private void ResetObjectVelocity(Transform obj)
    {
        var rb = obj.gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

    public void SpawnAsteroid()
    {


        int rNumber = Random.Range(0, AsteroidTypes.Count);
        Vector3 newLocation = Random.insideUnitCircle * 10;

        var newAsteroid = Instantiate(AsteroidTypes[rNumber].prefab, newLocation, quaternion.identity);
        var handle = newAsteroid.GetComponent<AsteroidHandle>();
        handle.Init();
        Asteroids.Add(handle);
        
    }
}
