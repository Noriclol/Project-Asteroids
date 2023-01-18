using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{

    [SerializeField] 
    private DifficultySetting settings;
    
    
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
        Vector3 targetDistance = new Vector3(Random.Range(-settings.AsteroidTargetSpawnRadius, settings.AsteroidTargetSpawnRadius), 0, Random.Range(-settings.AsteroidTargetSpawnRadius, settings.AsteroidTargetSpawnRadius));

        Vector3 targetArea = (Random.insideUnitCircle.normalized * settings.AsteroidTargetingOffset);

        var neeww = targetDistance + targetArea;
        
        int index = Random.Range(0, AsteroidTypes.Count);

        Vector3 spawnPosition = (Random.insideUnitCircle.normalized * settings.AsteroidSpawnDistance);

        Vector3 targetPosition = spawnPosition + (spawnPosition - transform.position).normalized * Vector3.Distance(spawnPosition, transform.position);
        
        
        
        var newAsteroid = Instantiate(AsteroidTypes[index].prefab, spawnPosition, quaternion.identity);
        var handle = newAsteroid.GetComponent<AsteroidHandle>();
        handle.Init(((transform.position - spawnPosition).normalized * 1000));
        Asteroids.Add(handle);
        
    }


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        
        Gizmos.DrawWireSphere(transform.position, settings.AsteroidSpawnDistance);
        
    }
}
