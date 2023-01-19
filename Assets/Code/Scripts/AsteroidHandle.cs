using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidHandle : MonoBehaviour
{
    public GameManager manager;
    
    public Asteroid asteroid;
    
    public float health;

    

    public void Init()
    {
        health = asteroid.health;
        
        var circlePos = (Vector3)Random.insideUnitCircle.normalized + transform.position;
        var targetPos = (circlePos - transform.position) * asteroid.fragmentationForce;
        var torque = Random.Range(-20, 20);
        var rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(targetPos);
        rb.AddTorque(torque);
    }

    public void Init(Vector2 direction, float torque = 20)
    {
        health = asteroid.health;
        var rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(direction);
        rb.AddTorque(Random.Range(-torque, torque));
    }
    
    private void CreateFragments()
    {
        var fragments = new List<AsteroidHandle>();
        
        for (var i = 0; i < asteroid.fragmentationAmount; i++)
        {
            var circlePos = (Vector3)Random.insideUnitCircle.normalized + transform.position;
            var instance = Instantiate(asteroid.fragmentation.prefab,  circlePos, Quaternion.identity );
            var rb = instance.GetComponent<Rigidbody2D>();
            var handle = instance.GetComponent<AsteroidHandle>();
            handle.Init();
            rb.AddForce((circlePos - transform.position) * asteroid.fragmentationForce);
            rb.AddTorque(Random.Range(-20, 20));
            fragments.Add(handle);
        }
        FindObjectOfType<GameManager>().Asteroids.AddRange(fragments);
    }
    
    private void Fracture()
    {
        // if Empty then return
        if (!asteroid.fragmentation)
            return;
        
        // Instantiate
        CreateFragments();
    }
    
    public void TakeDamage()
    {
        transform.DOShakePosition(0.1f, 0.1f);
        
        Debug.Log("AsteroidHealth before = " + health);

        health--;  
        
        Debug.Log("AsteroidHealth after = " + health);

        if (health > 0) 
            return;
        
        Fracture();
            
        // Play Animation
        var instance = Instantiate(asteroid.destroyAnim, transform.position, quaternion.identity);

        var scale = Random.Range(0.1f, 0.5f);
            
        instance.transform.localScale = instance.transform.localScale * scale;

        var particles = instance.GetComponent<ParticleSystem>();
        particles.Play();

        //animation.gameObject.transform.parent = transform;
        
        Debug.Log("Asteroid Destroyed");
        Destroy(gameObject);
            
        // gameObject.GetComponent<SpriteRenderer>().enabled = false;
        // gameObject.GetComponent<Collider2D>().enabled = false;

        //return;
    }
    
}
