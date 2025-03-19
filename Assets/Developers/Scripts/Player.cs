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
        // Beweging op de X-as (links/rechts) en Y-as (omhoog/omlaag)
        float moveX = Input.GetAxis("Horizontal"); // "A" en "D"
        float moveY = Input.GetAxis("Vertical");   // "W" en "S"

        // Beweeg alleen op de wereld X- en Y-as
        Vector3 move = new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;
        transform.position += move;

        // Beheer de rotatie op de X-as
        float targetRotationX = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            targetRotationX = 35f; // Naar boven
        }
        else if (Input.GetKey(KeyCode.S))
        {
            targetRotationX = -35f; // Naar beneden
        }

        // Interpoleer soepel naar de gewenste rotatie (alleen X-as aanpassen)
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