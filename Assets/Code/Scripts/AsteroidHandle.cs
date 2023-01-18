using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidHandle : MonoBehaviour
{
    public Asteroid asteroid;
    public float health;


    public void Init()
    {
        this.health = asteroid.health;
        
        Vector3 newpos = (Vector3)Random.insideUnitCircle.normalized + transform.position;

        gameObject.GetComponent<Rigidbody2D>().AddForce((newpos - transform.position) * asteroid.fragmentationForce);
        gameObject.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-20, 20));
    }

    public void Init(Vector2 direction, float torque = 20)
    {
        this.health = asteroid.health;
        gameObject.GetComponent<Rigidbody2D>().AddForce((direction));
        gameObject.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-torque, torque));
    }
    
    private List<GameObject> CreateFragments()
    {
        List<GameObject> fragments = new List<GameObject>();
        
        for (int i = 0; i < 3; i++)
        {
            Vector3 newpos = (Vector3)Random.insideUnitCircle.normalized + transform.position;
            var prefab = Instantiate(asteroid.fragmentation.prefab,  newpos, Quaternion.identity );
            prefab.GetComponent<AsteroidHandle>().Init();
            
            prefab.GetComponent<Rigidbody2D>().AddForce((newpos - transform.position) * asteroid.fragmentationForce);
            prefab.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-20, 20));
        }

        //var fragment = Instantiate(asteroid.fragmentation.prefab, Random.insideUnitCircle.normalized, Quaternion.identity);
        
        
        
        // for (int i = 0; i < asteroid.fragmentationAmount;)
        // { 
        //     var fragment = Instantiate(asteroid.fragmentation.prefab, Random.insideUnitCircle.normalized, Quaternion.identity);
        //    var handle = fragment.GetComponent<AsteroidHandle>();
        //    var rb = fragment.GetComponent<Rigidbody2D>();
        //    
        //    handle.Init();
        //    rb.AddForce(Random.insideUnitCircle.normalized * asteroid.fragmentationForce);
        //    rb.AddTorque(Random.Range(-20, 20));
        //    fragments.Add(fragment);
        // }
        return fragments;
    }
    
    private void Fracture()
    {
        // if Empty then return
        if (asteroid.fragmentation == null)
            return;
        
        // Instantiate
        var result = CreateFragments();
        
    }
    
    
    public void TakeDamage()
    {
        transform.DOShakePosition(0.1f, 0.1f);
        
        Debug.Log("AsteroidHealth before = " + health);
        health--;  
        Debug.Log("AsteroidHealth after = " + health);

        if (health <= 0)
        {
            
            Fracture();
            
            // Play Animation

            var animationGameObject = Instantiate(asteroid.destroyAnim, transform.position, quaternion.identity);

            var scale = Random.Range(0.1f, 0.5f);
            
            animationGameObject.transform.localScale = animationGameObject.transform.localScale * scale;
            var animation = animationGameObject.GetComponent<ParticleSystem>();
            animation.Play();
            //animation.gameObject.transform.parent = transform;
            
            Debug.Log("Asteroid Destroyed");
            Destroy(this.gameObject);

            
            // gameObject.GetComponent<SpriteRenderer>().enabled = false;
            // gameObject.GetComponent<Collider2D>().enabled = false;
            return;
        }
         
    }

    private void OnDestroy()
    {
        
    }




}
