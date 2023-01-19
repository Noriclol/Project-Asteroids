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
    private Rigidbody2D rigidBody;
    
    [SerializeField]
    private ParticleSystem thrusterRight;
    
    [SerializeField]
    private ParticleSystem thrusterLeft;
    
    
    [SerializeField]
    private GameControls controls;

    
    // Input fields
    public float thrustInput;
    public float turnInput;

    public float thrustMultiplier = 5f;
    public float turnMultiplier = 5f;

    private bool isThrusting => thrustInput >= 1;
    private bool isTurningLeft => turnInput >= 1;
    private bool isTurningRight => turnInput <= -1;
    
    // Movement fields
    private float thrust;
    private float velocity;
    private Vector2 direction;
    
    
    #region Unity

    private void OnEnable()
    {
        controls ??= new GameControls();

        if (!rigidBody)
            rigidBody = GetComponent<Rigidbody2D>();
        
        
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
        AnimateThrusters();

        
    }

    private void LateUpdate()
    {
    }
    #endregion
    
    
    private void Shoot(InputAction.CallbackContext obj)
    {
        var newProjectile = Instantiate(projectile, projectileSpawnpoint.position, transform.rotation);
        var projectileRB = newProjectile.GetComponent<Rigidbody2D>();
        
        projectileRB.AddForce((Vector2)transform.up * projectileForce + rigidBody.velocity);
    }

    private void AnimateThrusters()
    {

        if (isThrusting)
        {
            if (isTurningLeft)
            {
                thrusterLeft.Stop();
                thrusterRight.Play();
                return;
            }
            if (isTurningRight)
            {
                thrusterLeft.Play();
                thrusterRight.Stop();
                return;
            }
            thrusterLeft.Play();
            thrusterRight.Play();
            return;
        }
        thrusterLeft.Stop();
        thrusterRight.Stop();
    }
    
    private void Turn(InputAction.CallbackContext callbackContext)
    {
        turnInput = callbackContext.ReadValue<float>();

    }

    private void Thrust(InputAction.CallbackContext callbackContext)
    {
        //Debug.Log($"Thrust");
        thrustInput = callbackContext.ReadValue<float>();
    }
    

    private void ApplyForce()
    {
        rigidBody.AddForce(transform.up.normalized * (thrustInput * thrustMultiplier));
    }


    private void ApplyRotation()
    {
        rigidBody.SetRotation(rigidBody.rotation + turnInput * turnMultiplier);

    }




}
