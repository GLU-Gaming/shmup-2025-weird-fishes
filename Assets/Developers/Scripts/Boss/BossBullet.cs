using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed;
    private float lifeDuration;
    void Start()
    {
        speed = 20f;
        lifeDuration = 3.5f;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        lifeDuration -= Time.deltaTime;
        if (lifeDuration <= 0)
        {
            Destroy(gameObject);
        }
    }
}
