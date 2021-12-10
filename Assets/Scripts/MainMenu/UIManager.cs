using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIButtonStates {StaticState,ButtonDrop}
public enum UIPanelsStates {StaticState,Show,Back}
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("UI Components")]
    public GameObject playButton;
    [Header("UI Panels")]
    public GameObject levelSelection;
    [Header("GUI Animators")]
    public Animator UIAnimatorController;
    private void Awake()
    {
        Instance = this;
        UIAnimatorController.SetInteger("State", (int)UIButtonStates.ButtonDrop);
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }
    public void OnLevelSelectionMenu(bool can)
    {
        if (can)
        {
            UIAnimatorController.SetInteger("PanelState", (int)UIPanelsStates.Show);
        }
        else
        {
            UIAnimatorController.SetInteger("PanelState", (int)UIPanelsStates.Back);

        }
        playButton.SetActive(!playButton.activeSelf);
    }
    public void LoadLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("CurrentLevel",levelIndex);
        SceneManager.Instance.LoadGamePlay();
    }
   
}
