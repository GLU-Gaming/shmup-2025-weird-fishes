using UnityEngine;

public class StarBullet : MonoBehaviour
{
    //[SerializeField] private Transform firePoint;
    public float speed;
    private float lifeDuration;
    private Rigidbody rb;
    void Start()
    {
        speed = 10;
        lifeDuration = 3f;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //float step = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, firePoint.position, step);
        transform.position += transform.up * speed * Time.deltaTime;
        //Vector3 direction = (transform.position - firePoints[i].position).normalized;
        //rb.linearVelocity = direction * step;
        lifeDuration -= Time.deltaTime;
        if (lifeDuration <= 0)
        {
            Destroy(gameObject);
        }
    }
}
