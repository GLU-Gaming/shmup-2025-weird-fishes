using UnityEngine;

public class TentacleMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float moveDistance = 5f;

    private Vector3 startPos;
    private int direction = 1;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position += new Vector3(0, direction * moveSpeed * Time.deltaTime, 0);

        if (Mathf.Abs(transform.position.y - startPos.y) >= moveDistance)
        {
            direction *= -1;
        }
    }
}
