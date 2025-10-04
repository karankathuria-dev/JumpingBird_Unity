using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer mainMixer;
    public Toggle musicToggle;
    public Toggle soundToggle;

    private const string MusicKey = "MusicMuted";
    private const string SoundKey = "SoundMuted";

    void Start()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        // PlayerPrefs stores 1 for muted, 0 for unmuted.
        // We invert this for the toggle state.
        bool isMusicMuted = PlayerPrefs.GetInt(MusicKey, 0) == 1;
        bool isSoundMuted = PlayerPrefs.GetInt(SoundKey, 0) == 1;

        // Set the toggle states based on the saved mute status.
        musicToggle.isOn = !isMusicMuted;
        soundToggle.isOn = !isSoundMuted;

        // Apply the loaded settings to the mixer.
        // The logic is: if it is muted, set volume to -80. Otherwise, set it to 0.
        mainMixer.SetFloat("MusicVolume", isMusicMuted ? -80f : 0f);
        mainMixer.SetFloat("SoundVolume", isSoundMuted ? -80f : 0f);
    }

    public void ToggleMusic(bool isOn)
    {
        // When the toggle is ON (isOn == true), the volume should be 0f (unmuted).
       
        float volume = isOn ? 0f : -80f;
        mainMixer.SetFloat("MusicVolume", volume);

        // Save the state: 0 for ON (unmuted), 1 for OFF (muted).
        PlayerPrefs.SetInt(MusicKey, isOn ? 0 : 1);
    }

    public void ToggleSound(bool isOn)
    {
        // When the toggle is ON (isOn == true), the volume should be 0f (unmuted).
        float volume = isOn ? 0f : -80f;
        mainMixer.SetFloat("SoundVolume", volume);

        // Save the state: 0 for ON (unmuted), 1 for OFF (muted).
        PlayerPrefs.SetInt(SoundKey, isOn ? 0 : 1);
    }
}