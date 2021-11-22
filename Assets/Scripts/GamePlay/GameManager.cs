using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header ("Display Panels")]
    public GameObject gameOverPanel;
    SceneManager sceneManager;
    AudioManager audioSource;
    private void Awake()
    {
        sceneManager = GameObject.FindObjectOfType<SceneManager>();
        audioSource = FindObjectOfType<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource.StopPlay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BackToMenu()
    {
        sceneManager.LoadMainMenu();
    }
    public void Reload()
    {
        sceneManager.ReloadScene();
    }
}
