using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[SerializeField] private Transform firePoint;
    public float speed;
    private float lifeDuration;
    void Start()
    {
        speed = 10;
        lifeDuration = 3f;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        lifeDuration -= Time.deltaTime;
        if (lifeDuration <= 0 )
        {
            Destroy(gameObject);
        }
    }
}
