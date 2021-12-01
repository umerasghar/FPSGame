using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LoadLevel {Hallway,Garveyard}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static LoadLevel currentLevel;
    [Header("UI refs")]
    public Text scoreText;
    public Text nextLevelOption;
    public Text levelDescriptionText;
    [Header("Display Panels")]
    public GameObject gameOverPanel;
    public GameObject levelWonPanel;
    [Header("Managers")]
    SceneManager sceneManager;
    AudioManager audioSource;
    [Header("Gameplay Refs")]
    public Transform mainPlayer;
    public Levels[] levelReferences;
    [Header("Particle Systems")]
    public ParticleSystem zombieSpawnEffect;
    [HideInInspector]
    public Levels activeLevel;
    int setLevelIndex;
    private void Awake()
    {
        Instance = this;
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            setLevelIndex = PlayerPrefs.GetInt("CurrentLevel");
        }
        else
        {
            setLevelIndex = 0;
        }

        sceneManager =FindObjectOfType<SceneManager>();
        audioSource = FindObjectOfType<AudioManager>();

    }
    // Start is called before the first frame update
    void Start()
    {     
        switch (setLevelIndex)
        {
            case 0:
                currentLevel = LoadLevel.Hallway;
                break;
            case 1:
                currentLevel = LoadLevel.Garveyard;
                break;
        }
        ReInstantiateValues();
        if(audioSource!=null)
        audioSource.StopPlay();
       // Invoke("SpawnEnemies", 1f);
    }
    void ReInstantiateValues()
    {
        mainPlayer.gameObject.GetComponent<Player>().ResetPlayerValues();
      //  levelWonPanel.SetActive(false);
        activeLevel = levelReferences[(int)currentLevel];
        activeLevel.levelClear = false;
        activeLevel.levelFailed = false;
        activeLevel.levelGameObject.SetActive(true);
        activeLevel.levelDescription = "Reach the <color=red>" + activeLevel.levelScoreLimit+ "</color> Score to clear Level";
        levelDescriptionText.text = activeLevel.levelDescription;
        mainPlayer.position = activeLevel.playerPosition.position;
        SetEnemies(true);
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
        //if (activeLevel.levelClear||activeLevel.levelFailed)
        //{
        //    switch (setLevelIndex)
        //    {
        //        case 0:
        //            currentLevel = LoadLevel.Hallway;
        //            break;
        //        case 1:
        //            currentLevel = LoadLevel.Garveyard;
        //            break;
        //    }
        //    SetEnemies(false);
        //    Debug.Log(activeLevel.levelIndex);
        //    activeLevel.levelGameObject.SetActive(false);
        //    ReInstantiateValues();
        //    scoreText.text = "Score: 000";
        //}
        if (sceneManager != null)
        {
            //ReInstantiateValues();
            //scoreText.text = "Score: 000";
            sceneManager.ReloadScene();
        }
    }
    public void NextLevel()
    {
        if (activeLevel.isLastLevel)
        {
           // setLevelIndex = 0;
            PlayerPrefs.SetInt("CurrentLevel", 0);
        }
        else
        {
           // setLevelIndex = 1;
            PlayerPrefs.SetInt("CurrentLevel", 1);
        }
        Reload();

    }
    //public IEnumerator WaitForSpawn(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    SpawnEnemies();

    //}
    public void UpdateScore(int playerScore)
    {
        scoreText.text = "Score: "+playerScore;
        if (playerScore == activeLevel.levelScoreLimit)
        {
            activeLevel.levelClear = true;
            if (activeLevel.isLastLevel)
            {
              //  setLevelIndex = (int)LoadLevel.Hallway;
                nextLevelOption.text = "Proceed To Previous Level";
            }
            else
            {
               // setLevelIndex = (int)LoadLevel.Garveyard;
                nextLevelOption.text= "Proceed To Next Level";
            }
            levelWonPanel.SetActive(true);

            EventTriggers.onPlayerWon();
        }
    }
    public void SetEnemies(bool can)
    {
        foreach (Enemy enemy in activeLevel.levelEnemies)
        {
            enemy.gameObject.SetActive(can);
        }
    }
}
