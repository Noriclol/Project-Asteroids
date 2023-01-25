using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    
    // Game
    [SerializeField]
    private GameObject ShipPrefab;
    private GameObject ShipInstance;
    
    [SerializeField] 
    private DifficultySetting settings;

    [HideInInspector]
    public List<AsteroidHandle> Asteroids = new List<AsteroidHandle>();
    
    
    // Time
    [HideInInspector]
    public float GameTime;
    [HideInInspector]
    public float TimeSinceLastAsteroidSpawned;

    
    // Debug variables
    private Vector3 lastSpawnPos;
    private Vector3 lastTargetPos;
    private Vector3 lastTargetVector;
    
    
    
    
    private void Start()
    {
        
        var spawnPos = Random.insideUnitCircle * 10;
        //Spawn Player
        ShipInstance = Instantiate(ShipPrefab, spawnPos, quaternion.identity);
        
    }
    
    
    //private void 
    

    private void Update()
    {
        GameTime += Time.deltaTime;
        TimeSinceLastAsteroidSpawned += Time.deltaTime;

        if (TimeSinceLastAsteroidSpawned >= settings.SpawnTimeInterval && Asteroids.Count < settings.AsteroidsInCirculation)
        {
            Debug.Log("AsteroidCreated");
            SpawnAsteroid();

        }

        this.transform.position = ShipInstance.transform.position;
    }

    private void ResetObjectVelocity(Transform obj)
    {
        var rb = obj.gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

    public void SpawnAsteroid()
    {
        
        Vector3 spawnPosition = Random.insideUnitCircle.normalized * settings.AsteroidSpawnDistance;
        lastSpawnPos = spawnPosition;

        var targetAimDirection =  (ReturnTargetPos() - spawnPosition).normalized;
        lastTargetPos = ReturnTargetPos();
        lastTargetVector = targetAimDirection;
        // Spawn
        var index = Random.Range(0, settings.Asteroids.Count);
        
        var newAsteroid = Instantiate(settings.Asteroids[index].prefab, spawnPosition, quaternion.identity);
        var handle = newAsteroid.GetComponent<AsteroidHandle>();
        
        //Init and add Force
        handle.Init(((Vector3)targetAimDirection - transform.position).normalized * 1000);
        
        Asteroids.Add(handle);
        TimeSinceLastAsteroidSpawned = 0;
    }


    Vector3 ReturnTargetPos()
    {
        switch (settings.targetMode)
        {
            case DifficultySetting.TargetingMode.Center:
                return Vector2.zero;
            
            case DifficultySetting.TargetingMode.General:
                return Random.insideUnitCircle * settings.TargetingSize;
            
            case DifficultySetting.TargetingMode.Player:
                return  (Vector3)Random.insideUnitCircle + ShipInstance.gameObject.transform.position;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Asteroid"))
        {
            Debug.Log("Asteroid tagged");
            if(Asteroids.Remove(col.gameObject.GetComponent<AsteroidHandle>()))
                Destroy(col.gameObject);
        }
    }


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        
        Gizmos.DrawWireSphere(transform.position, settings.AsteroidSpawnDistance);
        
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, 15);
        
        
        
        
        
        Gizmos.color = Color.cyan;

        if (lastSpawnPos != Vector3.zero)
            DrawAim(lastSpawnPos);

            
        
        
        
        Gizmos.color = Color.magenta;
        
        if (lastTargetPos != Vector3.zero)
            DrawAim(lastTargetPos);
        
        
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(lastSpawnPos, lastTargetVector * Vector3.Distance(lastSpawnPos, lastTargetPos));
        
        
        
        
    }


    public void DrawAim(Vector3 position)
    {
    
        Gizmos.DrawLine(position, position + Vector3.up);
        Gizmos.DrawLine(position, position + Vector3.down);
        Gizmos.DrawLine(position, position + Vector3.left);
        Gizmos.DrawLine(position, position + Vector3.right);
    }

    public enum GameStates
    {
        Playing,
        Win,
        Lose
    }
    
    
}
