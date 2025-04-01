using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    private string sceneName;
    void Start()
    {
        //getting Scene name
        sceneName = SceneManager.GetActiveScene().name;
    }
    public void Quit()
    {
        
    }
    public void LoadScene()
    {

    }
}
