using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LoadLevel {Hallway,Garveyard}
public class GameManager : Singleton<GameManager>
{
    [Header("Display Panels")]
    public LoadLevel currentLevel;
    public GameObject gameOverPanel;
    SceneManager sceneManager;
    AudioManager audioSource;
    public Transform mainPlayer;
    public Levels[] levelReferences;

    private Levels activeLevel;
    private void Awake()
    {
        sceneManager =FindObjectOfType<SceneManager>();
        audioSource = FindObjectOfType<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        activeLevel = levelReferences[(int)currentLevel];
        activeLevel.levelGameObject.SetActive(true);
        mainPlayer.position = activeLevel.playerPosition.position;
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
