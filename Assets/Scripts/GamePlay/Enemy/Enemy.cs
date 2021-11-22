using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyController controller;
    public CharacterBase characterProperties;
    public Transform[] spawnPoints;
    private int spawnPointsLength;
    public float damagePoint;
    //public void ApplyDamage(float hitPoint, IDamage objectToDamage)
    //{
    //    objectToDamage.TakeDamage(hitPoint);
    //}

    //public void TakeDamage(float hitPoint)
    //{
    //    characterProperties.health -= hitPoint;
    //    Debug.Log(characterProperties.health);
    //    characterProperties.healthBar.value = characterProperties.health;
    //    characterProperties.healthBarCounter.text = characterProperties.health+"";
    //    if (characterProperties.health == 0)
    //    {
    //        characterProperties.isDead = true;
    //    }
    //}
    // Start is called before the first frame update
    void Start()
    {
    
        spawnPointsLength = spawnPoints.Length;
    }

    // Update is called once per frame
    void Update()
    {

        if (characterProperties.isDead)
        {
            characterProperties.isDead = false;
            controller.ChangeState(EnemyState.Dead);
            controller.FollowPlayer(false);
       
            Invoke("ReSpawn", 2.8f);
        }

    }
    void ReSpawn()
    {
 
        int point= Random.Range(0, spawnPointsLength);
        this.gameObject.transform.position = spawnPoints[point].position;
        controller.FollowPlayer(true);
        controller.ChangeState(EnemyState.Walk);
        Reset();

    }
    private void Reset()
    {
    
        characterProperties.health = 50;
        characterProperties.healthBarCounter.text = "50";
        characterProperties.healthBar.value = 50;
    }

}
