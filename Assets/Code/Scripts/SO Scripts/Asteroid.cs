using UnityEngine;

[CreateAssetMenu(fileName = "Unnamed Asteroid", menuName = "ScriptableObjects/Asteroids")]
public class Asteroid : ScriptableObject
{
    public GameObject prefab;
    
    public float health;
    
    public Asteroid fragmentation;
    
    public float fragmentationAmount;

    public float fragmentationForce;
    
    public GameObject destroyAnim;

}
