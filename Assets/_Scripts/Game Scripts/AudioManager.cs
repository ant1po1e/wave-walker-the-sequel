using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [Header("Audio Source")]
    public AudioSource musicSource;
    public AudioSource SFXsource;

    [Space]
    [Header("Audio Clips")]
    public List<AudioClip> audioClips;

    private Dictionary<string, AudioClip> audioClipDictionary;

    private void Start() 
    {
        audioClipDictionary = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in audioClips)
        {
            audioClipDictionary.Add(clip.name, clip);
        }

        if (audioClipDictionary.TryGetValue("bgm", out AudioClip bgm))
        {
            musicSource.clip = bgm;
            musicSource.Play();
        }
    }

    public void PlaySFX(string clipName)
    {
        if (audioClipDictionary.TryGetValue(clipName, out AudioClip clip))
        {
            SFXsource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("AudioClip not found: " + clipName);
        }
    }
}
