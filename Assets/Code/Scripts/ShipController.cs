using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    [SerializeField] 
    private GameObject projectile;

    [SerializeField] 
    private Transform projectileSpawnpoint;

    private float projectileForce = 10f;
    
    [SerializeField]
    private Rigidbody2D rigidbody;
    
    [SerializeField]
    private ParticleSystem BR;
    
    [SerializeField]
    private ParticleSystem BL;
    
    
    [SerializeField]
    private GameControls controls;

    
    // Input fields
    public float thrustInput = 0f;
    public float turnInput = 0f;

    public float thrustMultiplier = 5f;
    public float turnMultiplier = 5f;
    
    // Movement fields
    private float thrust;
    private float velocity;
    private Vector2 direction;
    
    
    #region Unity

    private void OnEnable()
    {
        if (controls == null)
            controls = new GameControls();

        if (rigidbody == null)
            rigidbody = GetComponent<Rigidbody2D>();
        
        
        controls.Enable();
        controls.Player.Turn.Enable();
        controls.Player.Thrust.Enable();
        controls.Player.Fire.Enable();
        
        controls.Player.Turn.performed += Turn;
        controls.Player.Turn.canceled += Turn;
        
        controls.Player.Thrust.performed += Thrust;
        controls.Player.Thrust.canceled += Thrust;

        controls.Player.Fire.performed += Shoot;

    }



    private void OnDisable()
    {
        controls.Player.Turn.performed -= Turn;
        controls.Player.Turn.canceled -= Turn;
        
        controls.Player.Thrust.performed -= Thrust;
        controls.Player.Thrust.canceled -= Thrust;
        
        controls.Player.Fire.performed -= Shoot;
        
        
        controls.Player.Turn.Disable();
        controls.Player.Thrust.Disable();
        controls.Player.Fire.Disable();
        controls.Disable();
         }
    
    
    private void FixedUpdate()
    {
        ApplyForce();
        ApplyRotation();
        
    }

    private void LateUpdate()
    {
        AnimateThrusters();
    }
    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawRay(projectileSpawnpoint.position, projectileForce * transform.up);
    // }

    #endregion
    
    
    private void Shoot(InputAction.CallbackContext obj)
    {
        var newProjectile = Instantiate(projectile, projectileSpawnpoint.position, transform.rotation);
        var projectileRB = newProjectile.GetComponent<Rigidbody2D>();
        
        projectileRB.AddForce((Vector2)transform.up * projectileForce + rigidbody.velocity);
    }

    private void AnimateThrusters()
    {
        if (thrustInput > 0)
        {
            BL.Play();
            BR.Play();
        }
        
        else if (turnInput < 0)
        {
            BL.Play();
            BR.Stop();
        }
        
        else if (turnInput > 0 )
        {
            BR.Play();
            BL.Stop();
        }

        else
        {
            BL.Stop();
            BR.Stop();
        }
    }
    
    private void Turn(InputAction.CallbackContext callbackContext)
    {
        //Debug.Log($"Turning");
        turnInput = callbackContext.ReadValue<float>();
        
        if (turnInput < 0)
        {
            BL.Play();
        }
        else if (turnInput > 0 )
        {
            BR.Play();
        }
        
        //Debug.Log($"Turn: {callbackContext.ReadValue<float>()}");
    }

    private void Thrust(InputAction.CallbackContext callbackContext)
    {
        //Debug.Log($"Thrust");
        thrustInput = callbackContext.ReadValue<float>();
        

        
        //Debug.Log($"Thrust: {callbackContext.ReadValue<float>()}");
    }
    

    private void ApplyForce()
    {
        rigidbody.AddForce(transform.up.normalized * (thrustInput * thrustMultiplier));
    }


    private void ApplyRotation()
    {
        rigidbody.SetRotation(rigidbody.rotation + turnInput * turnMultiplier);

    }




}
