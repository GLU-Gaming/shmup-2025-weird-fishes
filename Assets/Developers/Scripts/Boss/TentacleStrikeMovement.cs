using UnityEngine;

public class TentacleStrikeMovement : MonoBehaviour
{
    public float speed = 5f;
    private bool movingDown = true;

    void Update()
    {
        float moveDirection = movingDown ? -1 : 1;
        transform.position += new Vector3(0, moveDirection * speed * Time.deltaTime, 0);

        if (transform.position.y <= -5f || transform.position.y >= 5f)
        {
            movingDown = !movingDown;
        }
    }

    private void OnTriggerEnter(Collider other) // 3D collision
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.Hitted(2); // Meer damage
                Debug.Log("Player hit by tentacle strike!");
            }
        }
    }
}
