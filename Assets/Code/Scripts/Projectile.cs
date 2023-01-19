using UnityEngine;

public class Projectile : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnDestroy()
    {
        Debug.Log("Projectile AutoDestroyed");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Asteroid"))
        {
            Debug.Log("Asteroid hit");
            var handle = col.GetComponent<AsteroidHandle>();
            
            handle.TakeDamage();
            Destroy(gameObject);
        }
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.magenta;
    //     Gizmos.DrawRay(transform.position, transform.up);
    // }
}
