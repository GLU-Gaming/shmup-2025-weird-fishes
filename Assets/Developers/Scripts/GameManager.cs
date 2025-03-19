using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    // volume Profile object
    [SerializeField] private VolumeProfile volumeProfile;
    // post-processing VFX
    private Vignette vignette;
    void Awake()
    {
        // initialization of vignette
        volumeProfile.TryGet(out vignette);
        vignette.active = false;

    }

    void Update()
    {
        
    }

    // making screen's borders red 
    public void MakeScreenRed()
    {
        vignette.active = true;
    }
}
