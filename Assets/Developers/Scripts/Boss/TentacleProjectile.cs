using UnityEngine;

public class TentacleProjectile : MonoBehaviour
{
    public float speed = 7f;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<Player>().TakeDamage(1);
            //Destroy(gameObject);
        }
    }
}
