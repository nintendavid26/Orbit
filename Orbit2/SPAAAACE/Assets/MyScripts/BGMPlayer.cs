using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BGMPlayer : MonoBehaviour
{
    public List<AudioClip> Songs;
    public AudioClip CurrentSong;
    public AudioSource Source;

    private static BGMPlayer instance = null;
    public static BGMPlayer Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        Source = GetComponent<AudioSource>();
        CurrentSong = Songs[Application.loadedLevel];
        Source.loop = true;
        Source.clip = CurrentSong;
        Source.Play();
    }
    void OnLevelWasLoaded()
    {
        if (CurrentSong != Songs[Application.loadedLevel])
        {
            CurrentSong = Songs[Application.loadedLevel];
            Source.clip=CurrentSong;
            Source.Play();
        }
    }
    public void PlaySound(AudioClip Sound, float volume)
    {
        Source.PlayOneShot(Sound, volume);
    } 
}
