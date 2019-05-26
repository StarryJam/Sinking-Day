using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager0 : MonoBehaviour
{
    public static AudioManager0 audioManager = null;
    public AudioMixer audioMixer;

    private void Awake()
    {
        if (audioManager == null)
        {
            audioMixer = Resources.Load("Audio/MasterMixer") as AudioMixer;
        }
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void SetMasterVolum(float volum)
    {
        audioMixer.SetFloat("MasterVolume", volum);
    }

    public void SetMusicVolum(float volum)
    {
        audioMixer.SetFloat("MusicVolume", volum);
    }

}
