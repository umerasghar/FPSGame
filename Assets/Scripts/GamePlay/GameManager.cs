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
        sceneManager =FindObjectOfType<SceneManager>();
        audioSource = FindObjectOfType<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(audioSource!=null)
        audioSource.StopPlay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BackToMenu()
    {
        if(sceneManager!=null)
        sceneManager.LoadMainMenu();
    }
    public void Reload()
    {
        if (sceneManager != null)
            sceneManager.ReloadScene();
    }
}
