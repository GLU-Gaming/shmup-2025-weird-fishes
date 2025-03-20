using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    public float speed;
    void Start()
    {
        speed = 10;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
