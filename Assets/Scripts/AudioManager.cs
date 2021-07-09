using System;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    string currentScene;
    bool isPlayingBossMusic;


    protected virtual void Awake()
    {
        //GameObject doesn't persist (even when IsPersist = true) when script is inheriting singleton script
        //so script has its own singleton

        //if (instance == null)
        //    instance = this;
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        //DontDestroyOnLoad(gameObject);

        currentScene = SceneManager.GetActiveScene().name; 

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            //s.source.outputAudioMixerGroup = audioMixerGroup;
        }
    }

    public void Start()
    {
        if (currentScene == "MainMenu")
        {
            if (currentScene == "Level1")
            {
                FindObjectOfType<AudioManager>().Stop("Boss_Level_1_SFX");
            }
            else if (currentScene == "Level2")
            {
                FindObjectOfType<AudioManager>().Stop("Boss_Level_2_SFX");
            }
            
            Play("Main_Menu_BGM");
            Play("Tavern_Ambience_SFX");
        }
        if (currentScene == "Tutorial")
        {
            Play("Tutorial_BGM");
            Play("Tavern_Ambience_SFX");
        }
        if (currentScene == "Level1")
            Play("Level_1_BGM");
        if (currentScene == "Level2")
            Play("Level_2_BGM");
        if (currentScene == "Level3")
            Play("");
        if (currentScene == "LoseScene")
            Play("Lose_BGM");
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }


        s.source.Pause();
    }

    public void UnPause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.UnPause();
    }
}


[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;
    public AudioMixerGroup audioMixerGroup;

    [Range(0f, 1f)] public float volume;
    [Range(.1f, 3f)] public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
