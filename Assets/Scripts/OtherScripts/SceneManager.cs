using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : Singleton<SceneManager>
{
    private void Awake()
    {
        DontDestroyOnLoad(Instance);
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager manager= FindObjectOfType<GameManager>();
        if (manager != null)
        {
            Destroy(manager.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadMainMenu()
    {
      //  AudioManager.instance.AudioSource.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    public void LoadGamePlay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
    }
    public void ReloadScene()
    {
        Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene.name);
    }
}
