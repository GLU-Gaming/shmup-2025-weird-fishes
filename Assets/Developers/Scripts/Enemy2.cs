using UnityEngine;

public class Enemy2 : EnemyBase
{
    private int HP = 3;
    //[SerializeField] private Transform target;
    public float speed;
    void Start()
    {
        speed = 5;
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    void Update()
    {
        //float step = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
    public override void Shoot()
    {

    }
    public override void Destroyed()
    {
        //deleting enemy
        Destroy(gameObject);
    }
    public override void Spawn()
    {

    }
    public override void Damaged()
    {
        //damaging enemy
        HP--;
        if (HP <= 0)
        {
            Destroyed();
        }
    }

    //Enemy has been hitted
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enemy hitted");
        if (other.gameObject.CompareTag("Bullet"))
        {
            audioManager.PlaySound(0);
            Damaged();
        }
    }
}
