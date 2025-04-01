using UnityEngine;

public class EnvironmentMove : MonoBehaviour
{
    float speed;

    private void Start()
    {
        speed = 5f;
    }
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position += transform.right * -speed * Time.deltaTime;
        if (gameObject.transform.position.x <= -40)
        {
            gameObject.transform.position = new Vector3(55f, transform.position.y, transform.position.z);
        }
    }
}
