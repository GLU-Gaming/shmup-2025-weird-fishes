using UnityEngine;

public class Enemy1 : EnemyBase
{
    private int HP = 1;
    [SerializeField] private Transform target;
    public float speed;
    void Start()
    {
        speed = 5;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
    public override void Shoot()
    {

    }
    public override void Destroyed()
    {
        Destroy(gameObject);
    }
    public override void Spawn()
    {

    }
    public override void Damaged()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (other.gameObject.CompareTag("Player"))
        {
            Destroyed();
        }
    }
}
