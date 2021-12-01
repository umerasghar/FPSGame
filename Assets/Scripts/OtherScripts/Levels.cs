using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Levels
{
    [Header("Level References")]
    public GameObject levelGameObject;
    public string levelDescription;
    public int levelIndex;
    public int levelScoreLimit;
    [HideInInspector]
    public bool levelClear;
    [HideInInspector]
    public bool levelFailed;
    public bool isLastLevel;
    public Transform playerPosition;
    [Header("SpawnPoints")]
    public Transform[] enemySpawnPoints;
    [Header("LevelEnemies")]
    public Enemy[] levelEnemies;
}
