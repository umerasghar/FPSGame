using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public SoundComponents gameSounds;
    private AudioSource audioSource;

   // [HideInInspector]
    public AudioSource AudioSource { get => audioSource; set => audioSource = value; }

    private void Awake()
    {
        DontDestroyOnLoad(Instance);
    }
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = this.gameObject.GetComponent<AudioSource>();
        PlayBackGroundMusic();   
    }
    public void PlayBackGroundMusic()
    {
        AudioSource.loop = true;
        AudioSource.clip = gameSounds.GameBGMusic;
        AudioSource.Play();
    }
    public void ButtonClick()
    {
        AudioSource.loop = false;
        AudioSource.PlayOneShot(gameSounds.ButtonClick);
    }


}
