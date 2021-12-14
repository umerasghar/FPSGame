using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
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
    [Header("In-Game Managers")]
    public GameObject weaponManager;
    public GameObject eventManager;
    [Header("Particle Systems")]
    public ParticleSystem zombieSpawnEffect;
    [Header("TimeLine Refs")]
    public TimeLineComponents[] levelCutscenes;
    public Transform cineCameraTransform;
    public GameObject playableCharacterPos;
    [HideInInspector]
    public Levels activeLevel;
    int setLevelIndex;
    TimeLineComponents currentCutScene;
    private void Awake()
    {
        Time.timeScale = 1f;
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
        currentCutScene = levelCutscenes[setLevelIndex];
       

    }
    //private void OnEnable()
    //{
    //    playableDirector.stopped += PlayableDirector_stopped;
    //}
    //private void OnDisable()
    //{
    //    playableDirector.stopped -= PlayableDirector_stopped;
    //}
    //private void PlayableDirector_stopped(PlayableDirector obj)
    //{
    //    Debug.Log("Stopped");
    //}

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

        //  ReInstantiateValues();
        // playableDirector.Play();
        SetLevel();
        SetEnemies(true);
        StartCutScene();
        
        if (audioSource != null)
            audioSource.StopPlay();
        // Invoke("SpawnEnemies", 1f);
    }
    void SetLevel()
    {
        activeLevel = levelReferences[(int)currentLevel];
        activeLevel.levelClear = false;
        activeLevel.levelFailed = false;
        activeLevel.levelGameObject.SetActive(true);
        activeLevel.levelDescription = "Reach the <color=red>" + activeLevel.levelScoreLimit + "</color> Score to clear Level";
        levelDescriptionText.text = activeLevel.levelDescription;
    }
    public void StartCutScene()
    {
        #region Camera Transform
        cineCameraTransform.position = currentCutScene.startCameraPos.position;
        cineCameraTransform.eulerAngles = currentCutScene.startCameraPos.eulerAngles;
        cineCameraTransform.localScale = currentCutScene.startCameraPos.localScale;
        #endregion
        #region Character Transform
        playableCharacterPos.transform.position = currentCutScene.characterPos.position;
        playableCharacterPos.transform.eulerAngles = currentCutScene.characterPos.eulerAngles;
        playableCharacterPos.transform.localScale = currentCutScene.characterPos.localScale;
        #endregion
        currentCutScene.playableDirector.Play();

    }
    public void InitiateManager()
    {
        ReInstantiateValues();
    }
    void ReInstantiateValues()
    {
        mainPlayer.gameObject.GetComponent<Player>().ResetPlayerValues();
        //  levelWonPanel.SetActive(false);

        mainPlayer.position = activeLevel.playerPosition.position;
  
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
    public void OnPause(bool can)
    {
        if (can)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

}
