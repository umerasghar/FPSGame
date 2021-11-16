using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public SoundComponents gameSounds;
    private AudioSource audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(Instance);
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        PlayBackGroundMusic();   
    }
    public void PlayBackGroundMusic()
    {
        audioSource.loop = true;
        audioSource.clip = gameSounds.GameBGMusic;
        audioSource.Play();
    }
    public void ButtonClick()
    {
        audioSource.loop = false;
        audioSource.PlayOneShot(gameSounds.ButtonClick);
    }

}
