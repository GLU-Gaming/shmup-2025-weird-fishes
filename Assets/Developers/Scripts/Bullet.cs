using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private float lifeDuration;
    void Start()
    {
        speed = 25f;
        lifeDuration = 3.5f;
    }

    void Update()
    {
        // moves bullet
        float step = speed * Time.deltaTime;
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // destroy the bullet by the time
        lifeDuration -= Time.deltaTime;
        if (lifeDuration <= 0 )
        {
            Destroy(gameObject);
        }
    }
}
