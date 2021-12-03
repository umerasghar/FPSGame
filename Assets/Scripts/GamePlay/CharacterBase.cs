using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
[Serializable]

public class CharacterBase: MonoBehaviour, IDamage
{
    public enum CharacterType { Player, Enemy }
    public CharacterType playerType;
    public float health;
    public Animator characterAnimator;
    public Slider healthBar;
    public Text healthBarCounter;
    [HideInInspector]
    public bool isDead;

    public void TakeDamage(float hitPoint)
    {
        if (!isDead&&health!=0)
        {
            health -= hitPoint;
            Debug.Log(health);
            healthBar.value = health;
            healthBarCounter.text = health + "";
        }
        if (health == 0)
        {
            isDead = true;

        }

    }

    public void ApplyDamage(float hitPoint, IDamage objectToDamage)
    {
        objectToDamage.TakeDamage(hitPoint);
    }
    //public void Update()
    //{

    //}
    void OnCollisionEnter(Collision collider)
    {
        if (playerType == CharacterType.Player)
        {
            if (collider.gameObject.tag == "Enemy")
            {
              
               ApplyDamage(collider.gameObject.transform.root.gameObject.GetComponent<Enemy>().damagePoint, this.gameObject.GetComponent<IDamage>());
                this.CheckCharacterDeath();
                BloodSplatterEffect.instance.BloodSplatter();
               
            }
        }
    }
    public void CheckForDamage()
    {

    }
    public void CheckCharacterDeath()
    {
        switch (playerType)
        {
            case CharacterType.Player:
                this.GetComponent<Player>().OnPlayerHit();
                break;
            case CharacterType.Enemy:
                this.GetComponent<Enemy>().OnEnemyHit();
                break;
        }
    }
}
