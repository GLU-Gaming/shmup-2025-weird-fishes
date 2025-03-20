using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    //Sounds
    [SerializeField] private AudioSource audioSFX;
    [SerializeField] private AudioClip[] SFX;

    //Music
    [SerializeField] private AudioSource audioMusic;
    [SerializeField] private AudioClip[] soundtracks;

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
        if (soundIndex == 0)
        {
            audioSFX.PlayOneShot(SFX[0]);
        }
        else if (soundIndex == 1)
        {
            audioSFX.PlayOneShot(SFX[1]);
        }
    }

    // changing volume of audio source
    public void ChangeVolumeSound(string config = "up")
    {
        float currentVolume = audioMusic.volume;
        Debug.Log(currentVolume);
        //if (config == "up")
        //{
        //    audioMusic.volume = 1f;
        //}
        //else {
        //    audioMusic.volume = 0.2f;
        //}
    }
}
