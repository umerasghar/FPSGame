using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Levels
{
    [Header("Level References")]
    public GameObject levelGameObject;
    public int levelIndex;
    public Transform playerPosition;
}
