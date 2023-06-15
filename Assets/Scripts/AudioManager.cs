using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Insert Audio")]
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup soundEffectsMixerGroup;
    [SerializeField] private Sound[] sounds;

    public static AudioManager Instance;
    private bool muted = false;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }
    }
    private void Awake()
    {
        Instance = this;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.loop = s.isLoop;
            s.source.volume = s.volume;

            switch (s.audioType)
            {
                case Sound.AudioTypes.soundEffect:
                    s.source.outputAudioMixerGroup = soundEffectsMixerGroup;
                    break;

                case Sound.AudioTypes.music:
                    s.source.outputAudioMixerGroup = musicMixerGroup;
                    break;
            }

            if (s.playOnAwake)
                s.source.Play();
        }
    }

    public void Play(string clipname)
    {
        Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipname);
        if (s == null)
        {
            Debug.LogError("Sound: " + clipname + " does NOT exist!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string clipname)
    {
        Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipname);
        if (s == null)
        {
            Debug.LogError("Sound: " + clipname + " does NOT exist!");
            return;
        }
        s.source.Stop();
    }

    public void Pause()
    {
        foreach (Sound s in sounds)
        {
            if(s.audioType == Sound.AudioTypes.music)
            {
                s.source.Pause();
            }
        }
    }
    public void Resume()
    {
        foreach (Sound s in sounds)
        {
            if (s.audioType == Sound.AudioTypes.music)
            {
                s.source.UnPause();
            }
        }
    }

    public void ToggleMute()
    {
        muted = !muted;
        AudioListener.pause = muted;
        Save();
    }

    public void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    public void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }

    public void UpdateMixerVolume()
    {
        musicMixerGroup.audioMixer.SetFloat("bgmVol", Mathf.Log10(AudioOptionsManager.musicVolume) * 20);
        soundEffectsMixerGroup.audioMixer.SetFloat("soundfxVol", Mathf.Log10(AudioOptionsManager.soundEffectsVolume) * 20);
    }
}