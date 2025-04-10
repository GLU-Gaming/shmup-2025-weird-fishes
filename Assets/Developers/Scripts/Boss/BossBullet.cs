using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed;
    private float lifeDuration;
    void Start()
    {
        speed = 20f;
        lifeDuration = 3.5f;
        transform.rotation = Quaternion.Euler(0,90f,0);
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        lifeDuration -= Time.deltaTime;
        if (lifeDuration <= 0)
        {
            Destroy(gameObject);
        }
    }
}
