using UnityEngine;

public class Enemy1 : EnemyBase
{
    private int HP = 1; //health
    [SerializeField] private Transform target; //player
    public float speed;
    void Start()
    {
        speed = 5;
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    void Update()
    {
        // moving to the player
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
    public override void Shoot()
    {

    }
    public override void Destroyed()
    {
        // deleting object
        Destroy(gameObject);
    }
    public override void Spawn()
    {

    }
    public override void Damaged()
    {

    }

    // enemy triggered by other
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        // checking Tag of hitted object
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bullet"))
        {
            //Boom
            audioManager.PlaySound(0);
            audioManager.ChangeVolumeSound("d");
            Destroyed();
        }
        //else if (other.gameObject.CompareTag("Bullet"))
        //{
        //    //Boom
        //    audioManager.PlaySound(0);
        //    audioManager.ChangeVolumeSound("d");
        //    Destroyed();
        //}
    }
}
