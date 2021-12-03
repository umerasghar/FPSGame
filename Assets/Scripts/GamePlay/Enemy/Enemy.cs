using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyController controller;
    public CharacterBase characterProperties;
    public GameObject[] enemyVariants;
    public GameObject lastActiveEnemy;
    public float damagePoint;
    bool isLastEnemy;
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
    int spawnPoint;
    ParticleSystem spawnEffect;

    void Start()
    {
        EventTriggers.onPlayerWon += ShowPlayerWin;
        spawnEffect = GameManager.Instance.zombieSpawnEffect;
    }
    private void OnDisable()
    {
        EventTriggers.onPlayerWon -= ShowPlayerWin;
    }
    // Update is called once per frame
    //void Update()
    //{

    //    //if (characterProperties.isDead)
    //    //{
    //    //    characterProperties.isDead = false;
    //    //    controller.ChangeState(EnemyState.Dead);
    //    //    controller.FollowPlayer(false);


    //    //    //StartCoroutine(GameManager.Instance.WaitForSpawn(2.8f));
    //    //    if (!GameManager.Instance.activeLevel.levelClear)
    //    //    {
    //    //        controller.playerReference.playerScore += 100;
    //    //        GameManager.Instance.UpdateScore(controller.playerReference.playerScore);
    //    //    }

    //    //        if (!CheckLastEnemy())
    //    //        {
    //    //            spawnPoint = Random.Range(0, GameManager.Instance.activeLevel.enemySpawnPoints.Length);
    //    //            spawnEffect.transform.position = GameManager.Instance.activeLevel.enemySpawnPoints[spawnPoint].position;
    //    //            spawnEffect.Play();
    //    //            Invoke("ReSpawn", 2f);
    //    //        }
            
            
    //    //}

    //}
    public void OnEnemyHit()
    {
        if (characterProperties.isDead)
        {
            characterProperties.isDead = false;
            controller.ChangeState(EnemyState.Dead);
            controller.FollowPlayer(false);


            //StartCoroutine(GameManager.Instance.WaitForSpawn(2.8f));
            if (!GameManager.Instance.activeLevel.levelClear)
            {
                controller.playerReference.playerScore += 100;
                GameManager.Instance.UpdateScore(controller.playerReference.playerScore);
            }

            if (!CheckLastEnemy())
            {
                spawnPoint = Random.Range(0, GameManager.Instance.activeLevel.enemySpawnPoints.Length);
                spawnEffect.transform.position = GameManager.Instance.activeLevel.enemySpawnPoints[spawnPoint].position;
                spawnEffect.Play();
                Invoke("ReSpawn", 2f);
            }


        }
    }
    public bool CheckLastEnemy()
    {
        int currentScore = controller.playerReference.playerScore;
        int scoreLimit = GameManager.Instance.activeLevel.levelScoreLimit;
        if (currentScore+100 == scoreLimit||currentScore==scoreLimit)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public void ReSpawn()
    {
        this.gameObject.SetActive(false);
        // int enemyRange = Random.Range(0, GameManager.Instance.activeLevel.levelEnemies.Length);
        // spawnPoint = Random.Range(0, GameManager.Instance.activeLevel.enemySpawnPoints.Length);
        int enemyVariantIndex = Random.Range(0, enemyVariants.Length);
        if (lastActiveEnemy != null)
        {
            lastActiveEnemy.SetActive(false);          
        }
        lastActiveEnemy = enemyVariants[enemyVariantIndex];
        lastActiveEnemy.SetActive(true);
      //  this.gameObject.transform.position = GameManager.Instance.activeLevel.enemySpawnPoints[spawnPoint].position;
       // controller.FollowPlayer(true);
        controller.ChangeState(EnemyState.Idle);
        Invoke("Reset",1f);

    }
    private void Reset()
    {
        EventTriggers.onPlayerWon += ShowPlayerWin;
        this.gameObject.transform.position = GameManager.Instance.activeLevel.enemySpawnPoints[spawnPoint].position;
        this.gameObject.SetActive(true);
        controller.FollowPlayer(true);
        characterProperties.health = 50;
        characterProperties.healthBarCounter.text = "50";
        characterProperties.healthBar.value = 50;
    }
    public void ShowPlayerWin()
    {
      //  controller.ChangeState(EnemyState.Dead);
        characterProperties.isDead = true;
        //characterProperties.health = 50;
        //characterProperties.healthBarCounter.text = "50";
        //characterProperties.healthBar.value = 50;
        // controller.FollowPlayer(false);
    }

}
