using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public CharacterBase characterProperties;
    [HideInInspector]
    public int playerScore;
  //  bool triggerOnce;
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
        if (characterProperties.isDead)
        {
            GameManager.Instance.gameOverPanel.SetActive(true);
            GameManager.Instance.activeLevel.levelFailed = true;
            controller.playerRB.isKinematic = true;
            EventTriggers.onPlayerDead();
            characterProperties.isDead = false;
        }

    }
     public void ResetPlayerValues()
    {
        characterProperties.health = 100;
      characterProperties.healthBarCounter.text = "100";
   characterProperties.healthBar.value = 100;
     playerScore = 0;
    }

    //void OnCollisionEnter(Collision collider)
    //{
    //    if (characterProperties.playerType == CharacterBase.CharacterType.Player)
    //    {
    //        if (collider.gameObject.tag == "Enemy")
    //        {
    //            characterProperties.ApplyDamage(collider.gameObject.GetComponent<Enemy>().damagePoint, this.gameObject.GetComponent<IDamage>());
    //        }
    //    }
    //}
}
