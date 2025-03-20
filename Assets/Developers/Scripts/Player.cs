using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 5; // Player's HP
    public float moveSpeed = 5f; // Snelheid van de speler
    public float rotationSpeed = 5f; // Snelheid van de rotatie
    public GameManager gameManager;

     void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    void Update()
    {
        // Vertical movement (W = Up, S = Down)
        float moveY = Input.GetAxis("Vertical"); // "W" and "S"

        // Move player only on the Y-axis
        Vector3 move = new Vector3(0, moveY, 0) * moveSpeed * Time.deltaTime;
        transform.position += move;

        // Handle rotation on the X-axis
        float targetRotationX = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            targetRotationX = 35f; // Tilt upwards
        }
        else if (Input.GetKey(KeyCode.S))
        {
            targetRotationX = -35f; // Tilt downwards
        }

        // Smoothly interpolate to the target rotation (only X-axis changes)
        Quaternion targetRotation = Quaternion.Euler(targetRotationX, 0, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Player has been hitted by the enemy
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Hitted();
        }
    }
    private void Hitted()
    {
        health--; //Auch

        //spawning exsplosion particle 
        //Instantiate(BoomVFX);  

        switch (health)
        {
            case 1:
                gameManager.MakeScreenRed(); //Changing volume profile for "blood" effect
            break;

            case 0:
                // game over
            break;
        }
    }
}
