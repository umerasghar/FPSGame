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
	public int startBullets = 30;
	public float recoil = 0f;
	public float aimSpeed = 0.5f;
	[Header("Weapon Components")]
	public Animator animator;
	public Transform weaponTransform;
	public Transform shortAimTransform;
	[Header("Effects")]
	public ParticleSystem muzzleFalsh;
	public GameObject bloodEffect;
	public GameObject bulletImpact;
	public GameObject hitParticle;
	[Header("SFX")]
	public AudioClip fireSound;
	public AudioClip reloadSound;
	#region
	[HideInInspector]
	public int loadedBullets = 0;
	[HideInInspector]
	public bool hasLastFire = false;
	[HideInInspector]
	public bool isReloading = false;
	[HideInInspector]
	public bool canFire = false;
	private Vector3 originalPos;

    public void Start()
    {
		originalPos = weaponTransform.localPosition;
		loadedBullets = startBullets;
    }
    public void PlayFireSound()
    {
		//AudioManager.Instance.AudioSource.PlayOneShot(fireSound);
		Camera.main.GetComponent<AudioSource>().PlayOneShot(fireSound);
    }

    public void ShortAimSight(bool can)
    {
		if (can)
		{
			weaponTransform.localPosition = Vector3.Lerp(weaponTransform.localPosition, shortAimTransform.localPosition, Time.deltaTime * aimSpeed);
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 50, 2f);
		}
        else
        {
			weaponTransform.localPosition = Vector3.Lerp(weaponTransform.localPosition, originalPos, Time.deltaTime * aimSpeed);
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, 2f);
		}
    }

    public void ShowFireEffect()
    {
		muzzleFalsh.Play();
		animator.SetTrigger("shoot");
    }

    public void UpdateBullets()
    {
        if (loadedBullets!=0)
        {
			loadedBullets -= 1;
        }
        if (loadedBullets == 0)
        {
			Debug.Log(canFire);
			canFire = true;
			Debug.Log(canFire);
		}
        if (isReloading&&bulletsLeft!=0)
        {
			
			int bulletsNeeded = bulletsPerMag - loadedBullets;
			//isReloading = true;
            if (bulletsLeft > bulletsNeeded)
            {
				bulletsLeft -= bulletsNeeded;
				loadedBullets += bulletsNeeded;
            }
            else
            {
				bulletsLeft = 0;
				loadedBullets += bulletsLeft;
            }

        }
    }

    public void Update()
    {
   //     if (loadedBullets == 0)
   //     {
			//canFire = false;
   //     }
    }

    public void ReloadAnimation()
    {
		Camera.main.GetComponent<AudioSource>().PlayOneShot(reloadSound);
		animator.SetTrigger("Reload");

	}

    #endregion

}


