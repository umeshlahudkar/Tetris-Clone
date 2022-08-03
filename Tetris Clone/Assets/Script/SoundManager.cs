using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    [SerializeField] private AudioSource bgSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private soundType[] sounds;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        playBgSound(soundName.lobbyScene);
    }

    public void bgSourceMute(bool isMute)
    {
        bgSource.mute = isMute;
    }

    public void sfxSourceMute(bool isMute)
    {
        sfxSource.mute = isMute;
    }

    public void setBgSourceVolume(float value)
    {
        bgSource.volume = value;
    }

    public void setSfxSourceVolume(float value)
    {
        sfxSource.volume = value;
    }

    public void pauseSound()
    {
        bgSource.Pause();
        sfxSource.Pause();
    }

    public void unPauseSound()
    {
        bgSource.UnPause();
        sfxSource.UnPause();
    }


    public void playBgSound(soundName name)
    {
        AudioClip clip = getAudioClip(name);
        if(clip != null)
        {
            bgSource.clip = clip;
            bgSource.Play();
        }
        else
        {
            return;
        }
    }

    public void playSfxSound(soundName name)
    {
        AudioClip clip = getAudioClip(name);
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            return;
        }
    }

    public AudioClip getAudioClip(soundName soundName)
    {
        soundType name = Array.Find(sounds, item => item.name == soundName);
        if(name != null)
        {
            return name.soundClip;
        }
        else
        {
            return null;
        }
    }

    [Serializable]
    public class soundType
    {
        public soundName name;
        public AudioClip soundClip;
    } 
    public enum soundName
    {
        buttonClick,
        horrizontalMove,
        verticalMove,
        lobbyScene,
        rowCleared,
        gameOver,
        gamePlay
    }
}
