using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum Weapon { PISTOL, SHOTGUN, ASSULTRIFLE, SNIPER }
public class WeaponBase : MonoBehaviour, IWeapon
{
	[Header("Weapon Attributes")]
	public Weapon weaponType;
	public float damage = 17f;
	public float fireRate = 0.1f;
	public float range = 100f;
	public int bulletsPerMag = 30;
	public int bulletsLeft = 45;
	public int startBullets = 45;
	public float recoil = 0f;
	[Header("Weapon Components")]
	public Animator animator;
	public Transform weaponTransform;
	[Header("Effects")]
	public ParticleSystem muzzleFalsh;
	[Header("SFX")]
	public AudioClip fireSound;
	#region
	[HideInInspector]
	public int loadedBullets = 0;
	[HideInInspector]
	public bool hasLastFire = false;
	[HideInInspector]
	public bool isReloading = false;

    public void PlayFireSound()
    {
		//AudioManager.Instance.AudioSource.PlayOneShot(fireSound);
		Camera.main.GetComponent<AudioSource>().PlayOneShot(fireSound);
    }

    public void ShowFireEffect()
    {
		muzzleFalsh.Play();
    }

    public void UpdateBullets()
    {
        throw new NotImplementedException();
    }
    #endregion

}


