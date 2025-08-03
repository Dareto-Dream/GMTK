using UnityEngine;
using UnityEngine.Audio;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler Instance { get; private set; }
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource uiSource;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer mainMixer;

    [Header("Music Clips")]
    public AudioClip mainTheme;
    public AudioClip actionTheme;

    [Header("UI Clips")]
    public AudioClip UIClick;

    [Header("SFX Clips")]
    public AudioClip shoot;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // Uncomment if you want persistence across scenes:
        // DontDestroyOnLoad(gameObject);
    }

    // --- Music ---
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (musicSource == null) return;
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }
    public void StopMusic()
    {
        if (musicSource == null) return;
        musicSource.Stop();
    }

    // --- SFX ---
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource == null || clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    // --- UI SFX ---
    public void PlayUI(AudioClip clip)
    {
        if (uiSource == null || clip == null) return;
        uiSource.PlayOneShot(clip);
    }

    // --- Mixer Controls ---
    // Each expects volume 0.0 - 1.0
    public void SetMasterVolume(float volume)
    {
        if (mainMixer == null) return;
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f);
    }
    public void SetMusicVolume(float volume)
    {
        if (mainMixer == null) return;
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f);
    }
    public void SetSFXVolume(float volume)
    {
        if (mainMixer == null) return;
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f);
    }
    public void SetUIVolume(float volume)
    {
        if (mainMixer == null) return;
        mainMixer.SetFloat("UIVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f);
    }
}
