using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Snelheid van de speler
    public float rotationSpeed = 5f; // Snelheid van de rotatie

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
}