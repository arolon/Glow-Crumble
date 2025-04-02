using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

   public AudioSource audioSource;
    public AudioClip startSceneMusic;
    public AudioClip gameSceneMusic; // This will also play in PlayScene

    private string currentScene;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        PlayStartSceneMusic();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.name;

        if (currentScene == "GameScene" || currentScene == "PlayScene")
        {
            PlayGameSceneMusic();
        }
    }

    void PlayStartSceneMusic()
    {
        if (audioSource.clip != startSceneMusic)
        {
            audioSource.clip = startSceneMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void PlayGameSceneMusic()
    {
        if (audioSource.clip != gameSceneMusic)
        {
            audioSource.clip = gameSceneMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
