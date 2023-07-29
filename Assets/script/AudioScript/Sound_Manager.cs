using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{

    public static Sound_Manager Instance { get; private set; }
    [Header("-----Audio Source-----")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    [Header("-----Audio Clip-----")]
    public AudioClip background;
    public AudioClip finishSFX;
    public AudioClip carMovingSfx;
    public AudioClip coinHitSfx;
    public AudioClip carCrashSfx;
    public AudioClip oceanSfx;
    public AudioClip hornCarSFX;

    void Awake()
    {

        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayMusic(background);
    }


    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

}
