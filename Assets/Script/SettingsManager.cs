using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public AudioSource musicSource;
    public Slider musicSlider;
    public Toggle muteToggle;

    void Start()
    {
        settingsPanel.SetActive(false);

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

        
        musicSlider.onValueChanged.AddListener(SetVolume);
        muteToggle.onValueChanged.AddListener(SetMute);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
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
        if (isMuted)
        {
            musicSource.mute = true;
            musicSource.volume = 0f;
        }
        else
        {
            musicSource.mute = false;
            musicSource.volume = musicSlider.value;
        }

        PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }
}
