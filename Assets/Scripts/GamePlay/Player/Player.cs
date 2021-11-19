using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public CharacterBase characterProperties;
    //public void ApplyDamage(float hitPoint, IDamage objectToDamage)
    //{
    //    objectToDamage.TakeDamage(hitPoint);
    //}

    //public void TakeDamage(float hitPoint)
    //{
    //    characterProperties.health -= hitPoint;
    //    Debug.Log(characterProperties.health);
    //    characterProperties.healthBar.value = characterProperties.health;
    //    characterProperties.healthBarCounter.text = characterProperties.health + "";
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
