using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    //base variabeles for all enemy scripts
    public AudioManager audioManager;
    public GameManager gameManager;
    //public GameObject[] particles = new GameObject[5];

    //base functions for all enemy scripts
    public abstract void Shoot();
    public abstract void Destroyed();
    public abstract void Spawn();

    public abstract void Damaged();

    void FixedUpdate()
    {
        if (gameObject.transform.position.x <= -20)
        {
            Destroyed();
        }
    }
}
