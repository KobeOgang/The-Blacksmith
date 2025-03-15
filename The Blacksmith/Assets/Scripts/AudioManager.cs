using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;  
    public AudioSource sfxSource;  
    public AudioSource forgeFireSource; 

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip hammerSFX;
    public AudioClip forgeFireSFX;
    public AudioClip buttonClickSFX;

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
        PlayBGM(backgroundMusic);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip) return; 

        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // Start forge fire loop
    public void StartForgeFire()
    {
        if (!forgeFireSource.isPlaying)
        {
            forgeFireSource.clip = forgeFireSFX;
            forgeFireSource.loop = true;
            forgeFireSource.Play();
        }
    }

    
    public void StopForgeFire()
    {
        if (forgeFireSource.isPlaying)
        {
            forgeFireSource.Stop();
        }
    }
}
