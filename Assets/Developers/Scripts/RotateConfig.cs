using UnityEngine;

public class RotateConfig : MonoBehaviour
{
    [SerializeField] private Transform[] gearTrasforms1;
    [SerializeField] private Transform[] gearTrasforms2;
    private float rotationSpeed = 500f;
    private bool isRotating;
    private int gearRow;
    void Start()
    {
        isRotating = false;
    }

    void Update()
    {
        if (isRotating)
        {
            if (gearRow == 1)
            {
                gearTrasforms1[0].Rotate(0, 0, rotationSpeed * Time.deltaTime);
                gearTrasforms1[1].Rotate(0, 0, rotationSpeed * Time.deltaTime);
                gearTrasforms1[2].Rotate(0, 0, rotationSpeed * Time.deltaTime);
                gearTrasforms1[3].Rotate(0, 0, rotationSpeed * Time.deltaTime);
            }
            else {
                gearTrasforms2[0].Rotate(0, 0, rotationSpeed * Time.deltaTime);
                gearTrasforms2[1].Rotate(0, 0, rotationSpeed * Time.deltaTime);
                gearTrasforms2[2].Rotate(0, 0, rotationSpeed * Time.deltaTime);
            }
        }
    }
    public void Rotate(int num)
    {
        isRotating = true;
        if (num == 0)
        {
            gearRow = 1;
        }
        if (num == 1)
        {
            gearRow = 2;
        }
    }
    public void StopRotation()
    {
        isRotating = false;
    }
}
