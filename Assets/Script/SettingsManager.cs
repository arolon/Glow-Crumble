using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;  // The panel that will appear/disappear
    public AudioSource musicSource;
    public Slider musicSlider;
    public Toggle muteToggle;

    void Start()
    {
        settingsPanel.SetActive(false); // Ensure it's hidden at the start

        // Load saved settings
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("MusicVolume");
            musicSlider.value = savedVolume;
            musicSource.volume = savedVolume;
        }

        if (PlayerPrefs.HasKey("IsMuted"))
        {
            bool isMuted = PlayerPrefs.GetInt("IsMuted") == 1;
            muteToggle.isOn = isMuted;
            musicSource.mute = isMuted;
        }

        // Add listeners
        musicSlider.onValueChanged.AddListener(SetVolume);
        muteToggle.onValueChanged.AddListener(SetMute);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf); // Toggle the panel visibility
    }


    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        if (!muteToggle.isOn)
        {
            musicSource.volume = volume;
            PlayerPrefs.SetFloat("MusicVolume", volume);
            PlayerPrefs.Save();
        }
    }

    public void SetMute(bool isMuted)
    {
        // When muting, set the volume to 0, and unmute, set the volume to the current slider value
        if (isMuted)
        {
            musicSource.mute = true;
            musicSource.volume = 0f;  // Set volume to 0 when muted
        }
        else
        {
            musicSource.mute = false;
            musicSource.volume = musicSlider.value; // Set volume to slider value when unmuted
        }

        // Save mute state
        PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }
}
