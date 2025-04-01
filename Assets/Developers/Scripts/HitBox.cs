using UnityEngine;

public class HitBox : MonoBehaviour
{
    private Enemy1 enemy;

    void Start()
    {
        enemy = GetComponentInParent<Enemy1>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player Bullet"))
        {
            enemy.Damaged();
            Destroy(other.gameObject);
        }
    }
}
