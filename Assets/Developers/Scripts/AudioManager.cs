using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class AudioManager : MonoBehaviour
{
    //Sounds
    [SerializeField] private AudioSource audioSFX;
    [SerializeField] private AudioClip[] SFX;

    //Music
    [SerializeField] private AudioSource audioMusic;
    [SerializeField] private AudioClip[] soundtracks;

    // logic variabels for Lerp (smooth change)
    private float targetValueUp = 1f;
    private float targetValueDown = 0.2f;
    public float timeDuration = 1f;
    private float elapsedTime = 0f;
    private float currentVolume;
    [SerializeField] private string changeConfig;

    void Start()
    {
        //getting Scene name
        string currentScene = SceneManager.GetActiveScene().name;

        //Setting soundtrack depending on a scene
        if (currentScene == "Level1")
        {
            audioMusic.clip = soundtracks[0];

        } else if (currentScene == "Level2")
        {
            audioMusic.clip = soundtracks[1];
        } else if (currentScene == "Level3")
        {
            audioMusic.clip = soundtracks[2];
        } else if (currentScene == "Menu")
        {
            audioMusic.clip = soundtracks[3];
        } else if (currentScene == "GameOver")
        {
            audioMusic.clip = soundtracks[4];
        }
        // "𝄞 Music sounds better with you 𝄞"
        audioMusic.Play();
    }

    // play sound from the array
    public void PlaySound(int soundIndex)
    {
     

        switch (soundIndex)
        {
            case 0:
                audioSFX.PlayOneShot(SFX[0]);
                break;
            case 1:
                audioSFX.PlayOneShot(SFX[1]);
                break;
            case 2:
                audioSFX.PlayOneShot(SFX[2]);
                break;
            case 3:
                audioSFX.PlayOneShot(SFX[3]);
                break;

        }
    }
     void Update()
    {
        //Debug.Log(audioMusic.volume);


        // smooth change volume to max
        if (elapsedTime < timeDuration && changeConfig == "upper")
        {
            elapsedTime += Time.deltaTime;
            float changedVolume = Mathf.Lerp(currentVolume, targetValueUp, elapsedTime / timeDuration);
            audioMusic.volume = changedVolume;
            // smooth change volume to low
        } else if (elapsedTime < timeDuration && changeConfig == "downer")
        {
            elapsedTime += Time.deltaTime;
            float changedVolume = Mathf.Lerp(currentVolume, targetValueDown, elapsedTime / timeDuration);
            audioMusic.volume = changedVolume;
        }
        // reset
        if (elapsedTime >= timeDuration)
        {
            changeConfig = "null";
            elapsedTime = 0;
        }
    }

    // changing volume of audio source
    public void ChangeVolumeSound(string config = "up")
    {
        currentVolume = audioMusic.volume;
        Debug.Log(currentVolume);
        if (config == "up")
        {
            changeConfig = "upper";
        }
        else
        {
            changeConfig = "downer";
        }
    }
}
