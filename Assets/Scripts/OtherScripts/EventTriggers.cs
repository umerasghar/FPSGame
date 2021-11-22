using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventTriggers: Singleton<EventTriggers>
{
    public delegate void ShowGameOver();
    public static ShowGameOver onPlayerDead;
    private void Awake()
    {
        DontDestroyOnLoad(Instance);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
