using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class SoundComponents 
{
    [Header("Game Music")]
    [SerializeField]
    private AudioClip gameBGMusic;
    [SerializeField]
    [Header("SFX")]
    private AudioClip buttonClick;
    public AudioClip GameBGMusic { get => gameBGMusic; set => gameBGMusic = value; }
    public AudioClip ButtonClick { get => buttonClick; set => buttonClick = value; }
}

