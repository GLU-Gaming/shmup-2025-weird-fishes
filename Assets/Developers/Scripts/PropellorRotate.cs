using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Variabele om de rotatiesnelheid te beheren (aanpasbaar vanuit de Unity Inspector)
    public float rotationSpeed = 50f;

    void Update()
    {
        // Draait het object continu rond de X-as
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
