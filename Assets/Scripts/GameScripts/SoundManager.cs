using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioSource;
    public AudioClip buttonClickSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional: keep across scenes
        }
        
    }

    public void PlayButtonClick()
    {
        // Check if the music is supposed to be on
        bool isMusicOn = PlayerPrefs.GetInt("SoundMuted", 0) == 0;
        if (audioSource != null && buttonClickSound != null && isMusicOn)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
