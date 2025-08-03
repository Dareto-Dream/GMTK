using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler Instance { get; private set; }
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music Clips")]
    public AudioClip mainTheme;
    public AudioClip actionTheme;

    [Header("Music Clips")]
    public AudioClip UIClick;
    public AudioClip shoot;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
