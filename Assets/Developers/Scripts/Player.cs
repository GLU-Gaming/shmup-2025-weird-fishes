﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int health = 5; // Player's HP
    public float fireCooldown = 0.75f;
    public float moveSpeed = 5f; // Snelheid van de speler
    public float rotationSpeed = 5f; // Snelheid van de rotatie
    public float speed;
    public GameManager gameManager;
    public AudioManager audioManager;
    public GameObject[] kamikadzes;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject[] particles;
    private Rigidbody rb;

    void Start()
    {
        speed = 5f;
        rb = GetComponent<Rigidbody>();
        gameManager = FindFirstObjectByType<GameManager>();
        audioManager = FindFirstObjectByType<AudioManager>();
        kamikadzes = GameObject.FindGameObjectsWithTag("Kamikadze");
    }
    void Update()
    {

        // Vertical movement (W = Up, S = Down)
        float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        //// Move player only on the Y-axis
        transform.position += new Vector3(0, moveY, 0);

        //Y-axis borders
        float clampedY = Mathf.Clamp(transform.position.y, -11f, 11f);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);

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

        if (Input.GetKey(KeyCode.Space) && fireCooldown <= 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 270, 0));
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, 270f);

            fireCooldown = 0.75f;
        }

        // Smoothly interpolate to the target rotation (only X-axis changes)
        Quaternion targetRotation = Quaternion.Euler(targetRotationX, 0, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        fireCooldown -= Time.deltaTime;
    }
    //void FixedUpdate()
    //{
    //    float moveY = Input.GetAxis("Vertical"); // "W" and "S"

    //    Vector3 move = new Vector3(0, moveY, 0) * moveSpeed * Time.fixedDeltaTime;

    //    rb.MovePosition(rb.position + move);
    //}

    // Player has been hitted by the enemy
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Hitted();
        }

        if (other.gameObject.CompareTag("Enemy Bullet"))
        {
            //Boom
            Hitted();
            audioManager.ChangeVolumeSound("d");
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Kamikadze"))
        {
            SphereCollider explosionRadius = other.GetComponent<SphereCollider>();
            BoxCollider kamikadzeCollider = other.GetComponent<BoxCollider>();

            if (explosionRadius != null && other == explosionRadius)
            {
                Debug.Log("Ready?");
                audioManager.PlaySound(1);
            }
            if (kamikadzeCollider != null && other == kamikadzeCollider)
            {
                audioManager.ChangeVolumeSound("down");
                Hitted();
                Debug.Log("BABAH");
                gameManager.Stunned();
                gameManager.spawnedEnemies.Remove(other.gameObject);
                Destroy(other.gameObject);
                audioManager.PlaySound(2);
            }
        }

    }
    private void Hitted()
    {
        health--; //Auch

        //audioManager.PlaySound(0);

        //spawning exsplosion particle 
        //Instantiate(BoomVFX);  

        switch (health)
        {
            case 1:
                gameManager.MakeScreenRed(); //Changing volume profile for "blood" effect
            break;

            case 0:
                // game over
                SceneManager.LoadScene("GameOver");
            break;
        }
    }
}
