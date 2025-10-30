using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip clickSound;
    public AudioClip collectSound;
    public AudioClip backgroundMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(backgroundMusic);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null || musicSource == null) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.volume = 0.5f;
        musicSource.Play();
    }

    public void StopMusic(float fadeOutTime = 1f)
    {
        if (musicSource != null && musicSource.isPlaying)
            StartCoroutine(FadeOutMusic(fadeOutTime));
    }

    private IEnumerator FadeOutMusic(float duration)
    {
        float startVolume = musicSource.volume;

        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume;
    }

    public void PlayClick()
    {
        if (clickSound != null && sfxSource != null)
            sfxSource.PlayOneShot(clickSound);
    }

    public void PlayCollect()
    {
        if (collectSound != null && sfxSource != null)
            sfxSource.PlayOneShot(collectSound);
    }
    public AudioSource GetMusicSource()
    {
        return musicSource;
    }

}
