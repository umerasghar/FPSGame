using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EventTriggers: Singleton<EventTriggers>
{
    public delegate void ShowGameOver();
    public delegate void ShowPlayerWin();
    public delegate void ResetEnemy();
    public static ShowGameOver onPlayerDead;
    public static ShowPlayerWin onPlayerWon;
    public static ResetEnemy onEnemyReset;
    
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
